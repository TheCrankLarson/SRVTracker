using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Device.Location;
using EDTracking;
using System.Text.Json;

namespace DataCollator
{
    class NotificationServer
    {
        private HttpListener _Listener = null;
        private int _ListenPort = 11938;
        private List<string> _registeredNotificationUrls = new List<string>();
        private static HttpClient _httpClient = new HttpClient();
        private Dictionary<string, EDEvent> _playerStatus = new Dictionary<string, EDEvent>(); //  Store last known status (with coordinates) of client
        private Dictionary<string, EDRaceStatus> _commanderStatus = new Dictionary<string, EDRaceStatus>();
        private readonly object _notificationLock = new object();
        private FileStream _logStream = null;
        private Dictionary<Guid, EDRace> _races;
        private DateTime _lastStaleDataCheck = DateTime.Now;

        public NotificationServer(string ListenURL, bool EnableDebug = false)
        {
            if (EnableDebug)
                _logStream = File.Open("stream.log", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            URi = ListenURL;
            _races = new Dictionary<Guid, EDRace>();
            _Listener = new HttpListener();

            Start();
        }

        ~NotificationServer()
        {
            Stop();
            if (_logStream!=null)
            {
                try
                {
                    _logStream.Close();
                }
                catch { }
                _logStream = null;
            }
        }

        public void Start()
        {
            _Listener.Prefixes.Clear();
            _Listener.Prefixes.Add(URi);
            try
            {
                _Listener.Start();
                _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);
                Log($"Started listening on: {URi}");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Unable to start HTTP listener" + Environment.NewLine + "(are you running as administrator?)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Stop()
        {
            // Release the listener
            try
            {
                _Listener.Stop();
                _Listener.Close();
            }
            catch { }
            Log("Stopped listening");
        }

        public int Port
        {
            get
            {
                return _ListenPort;
            }
            set
            {
                if (value == _ListenPort) return;
                _Listener.Stop();
                _ListenPort = value;
                Start();
            }
        }

        public string URi { get; } = "";

        private void UpdateCommanderStatus(string status)
        {
            EDEvent updateEvent = null;
            try
            {
                updateEvent = EDEvent.FromJson(status);
                if (String.IsNullOrEmpty(updateEvent.Commander))
                    return;
            }
            catch (Exception ex)
            {
                Log($"Error creating event: {ex.Message}");
                return;
            }

            if (_races.Count > 0)
                Task.Run(new Action(() =>
                {
                    foreach (EDRace race in _races.Values)
                        race.UpdateStatus(updateEvent);
                }));

            if (!_commanderStatus.ContainsKey(updateEvent.Commander))
            {
                lock (_notificationLock)
                    _commanderStatus.Add(updateEvent.Commander, new EDRaceStatus(updateEvent));
                return;
            }

            if (_commanderStatus[updateEvent.Commander].TimeStamp > updateEvent.TimeStamp)
                return;

            lock (_notificationLock)
            {
                _commanderStatus[updateEvent.Commander].UpdateStatus(updateEvent);
                if (_playerStatus.ContainsKey(updateEvent.Commander))
                    _playerStatus[updateEvent.Commander] = updateEvent;
                else
                    _playerStatus.Add(updateEvent.Commander, updateEvent);
            }
        }

        public void ProcessNotification(string message)
        {
            // Log the notification for retrieval by clients that are polling
            try
            {
                UpdateCommanderStatus(message);
            }
            catch { }

            // Send the notification to any listening Urls
            if (_registeredNotificationUrls.Count > 0)
            {
                Log($"Sending notification to {_registeredNotificationUrls.Count} clients");
                StringContent notificationContent = new StringContent(message);
                foreach (string listenerUrl in _registeredNotificationUrls)
                {
                    _httpClient.PostAsync(listenerUrl, notificationContent); // Async, we don't want to wait (otherwise one unavailable host could affect others)
                }
            }
        }

        public void ListenerCallback(IAsyncResult result)
        {
            try
            {
                HttpListener listener = (HttpListener)result.AsyncState;

                HttpListenerContext context = listener.EndGetContext(result);
                HttpListenerRequest request = context.Request;
                string sRequest = "";

                using (StreamReader reader = new StreamReader(request.InputStream))
                    sRequest = reader.ReadToEnd();
                string requestUri = request.RawUrl.ToLower();

                Action action;
                if (requestUri.StartsWith("/datacollator/status"))
                {
                    // This is a request for all known locations/statuses of clients 
                    action = (() => {
                        SendStatus(context);
                    });
                }
                else if (requestUri.StartsWith("/datacollator/racestatus"))
                {
                    action = (() => {
                        SendRaceStatus(requestUri, context, sRequest);
                    });
                }
                else if (requestUri.StartsWith("/datacollator/startrace"))
                {
                    action = (() => {
                        StartRace(sRequest, context);
                    });
                }
                else if (requestUri.StartsWith("/datacollator/resurrectcommander"))
                {
                    if (requestUri.Length>33)
                    {
                        action = (() =>
                        {
                            ResurrectCommander(requestUri.Substring(33), context);
                        });
                    }
                    else
                    {
                        action = (() =>
                        {
                            WriteErrorResponse(context.Response, HttpStatusCode.NotFound);
                        });
                    }
                }
                else if (requestUri.StartsWith("/datacollator/getrace"))
                {
                    if (requestUri.Length > 22)
                    {
                        // Guid can be specified in the Url or in POST data.  This one has something in the Url
                        Guid raceGuid = Guid.Empty;
                        Guid.TryParse(requestUri.Substring(22), out raceGuid); 
                        action = (() =>
                        {
                            GetRace(raceGuid, context);
                        });
                    }
                    else
                    {
                        // No Guid in the URL, so check request content
                        action = (() =>
                        {
                            GetRace(sRequest, context);
                        });
                    }
                }
                else if (requestUri.StartsWith("/datacollator/getcommanderraceevents"))
                {
                    if (requestUri.Length > 37)
                    {
                        // Guid can be specified in the Url or in POST data.  This one has something in the Url
                        try
                        {
                            string[] requestParams = requestUri.Substring(37).Split('/');
                            Guid raceGuid = Guid.Empty;
                            Guid.TryParse(requestParams[0], out raceGuid);
                            action = (() =>
                            {
                                GetCommanderRaceEvents(raceGuid, requestParams[1], context);
                            });
                        }
                        catch
                        {
                            action = (() =>
                            {
                                WriteErrorResponse(context.Response, HttpStatusCode.NotFound);
                            });
                        }
                    }
                    else
                    {
                        action = (() =>
                        {
                            GetCommanderRaceEvents(sRequest, context);
                        });
                    }
                }
                else
                    action = (() => {
                        WriteResponse(context, $"{Application.ProductName} v{Application.ProductVersion}");
                    });
                Task.Run(action);
            }
            catch { }
            _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);
        }

        private void WriteErrorResponse(HttpListenerResponse httpResponse, HttpStatusCode errorCode)
        {
            httpResponse.StatusCode = (int)errorCode;
            httpResponse.ContentLength64 = 0;
            httpResponse.Close();
        }

        private void WriteResponse(HttpListenerResponse httpResponse, string response, int returnStatusCode = (int)HttpStatusCode.OK)
        {
            try
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(response);
                httpResponse.ContentLength64 = buffer.Length;
                httpResponse.StatusCode = returnStatusCode;

                using (Stream output = httpResponse.OutputStream)
                    output.Write(buffer, 0, buffer.Length);
                httpResponse.OutputStream.Flush();
                httpResponse.KeepAlive = true;
                httpResponse.Close();
            }
            catch { }
        }

        private void WriteResponse(HttpListenerContext Context, string response, int returnStatusCode = (int)HttpStatusCode.OK)
        {
            WriteResponse(Context.Response, response, returnStatusCode);
        }

        private void StartRace(string request, HttpListenerContext Context)
        {
            // Client has requested to start race monitoring.  The request should be an EDRace json.  We return a Guid

            try
            {
                EDRace race = EDRace.FromString(request);
                Guid raceId = Guid.NewGuid();
                _races.Add(raceId, race);
                race.StartRace(true);
                WriteResponse(Context, raceId.ToString());
            }
            catch (Exception ex)
            {
                WriteResponse(Context,$"Error while initialising race:{Environment.NewLine}{ex}", (int)HttpStatusCode.InternalServerError);
            }
        }

        private void GetRace(Guid raceGuid, HttpListenerContext Context)
        {
            if (raceGuid != Guid.Empty && _races.ContainsKey(raceGuid))
                WriteResponse(Context, _races[raceGuid].ToString());
            else
                WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
        }

        private void GetRace(string request, HttpListenerContext Context)
        {
            Guid raceGuid = Guid.Empty;
            Guid.TryParse(request, out raceGuid);
            GetRace(raceGuid, Context);
        }

        private void ResurrectCommander(string commander, HttpListenerContext Context)
        {
            string[] requestParams = commander.Split('/');
            Guid raceGuid = Guid.Empty;
            if (Guid.TryParse(requestParams[0], out raceGuid))
                if (_races.ContainsKey(raceGuid) && _races[raceGuid].Statuses.ContainsKey(requestParams[1]))
                {
                    _races[raceGuid].Statuses[requestParams[1]].Resurrect();
                    WriteResponse(Context, $"{requestParams[1]} added back to race");
                    return;
                }
            WriteResponse(Context, "Race or commander not found");  // We don't raise an error here as result is discarded client-side anyway
        }

        private void GetCommanderRaceEvents(Guid raceGuid, string commander, HttpListenerContext Context)
        {
            if (raceGuid != Guid.Empty && _races.ContainsKey(raceGuid))
                WriteResponse(Context, JsonSerializer.Serialize(_races[raceGuid].GetCommanderEventHistory(commander)));
            else
                WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
        }

        private void GetCommanderRaceEvents(string raceGuid, string commander, HttpListenerContext Context)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(raceGuid, out guid);
            GetCommanderRaceEvents(guid, commander, Context);
        }

        private void GetCommanderRaceEvents(string request, HttpListenerContext Context)
        {
            string[] requestParams = request.Split(':');
            GetCommanderRaceEvents(requestParams[0], requestParams[1], Context);
        }

        private void SendRaceStatus(string requestUri, HttpListenerContext Context, string request)
        {
            Guid raceGuid = Guid.Empty;
            if (requestUri.Length > 25)
            {
                // Guid can be specified in the Url or in POST data.  This one has something in the Url
                Guid.TryParse(requestUri.Substring(25), out raceGuid);
            }
            else
            {
                // Check if we have Guid in POST payload
                Guid.TryParse(request, out raceGuid);
            }
            if (raceGuid != Guid.Empty && _races.ContainsKey(raceGuid))
                WriteResponse(Context, _races[raceGuid].ExportRaceStatistics());
            else
                WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
            
            if (DateTime.Now.Subtract(_lastStaleDataCheck).TotalMinutes > 120)
                Task.Run(new Action(()=> { ClearStaleData(); }));
        }

        private void SendStatus(HttpListenerContext Context)
        {
            // Send Commander status

            StringBuilder status = new StringBuilder();

            // Check if a client Id was specified
            if (Context.Request.RawUrl.Length > 21)
            {
                string clientId = System.Web.HttpUtility.UrlDecode(Context.Request.RawUrl).Substring(21).ToLower();
                if (_playerStatus.ContainsKey(clientId))
                {
                    Log($"Player status requested: {clientId}");
                    status.AppendLine(_playerStatus[clientId].ToJson());
                }
                else
                {
                    Log($"Status requested for invalid client: {clientId}");
                    WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
                    return;
                }
            }
            else
            {
                Log("All player status requested");
                foreach (string id in _playerStatus.Keys)
                    status.AppendLine(_playerStatus[id].ToJson());
            }

            WriteResponse(Context, status.ToString());
            if (DateTime.Now.Subtract(_lastStaleDataCheck).TotalMinutes > 120)
                ClearStaleData();
        }

        private void ClearStaleData()
        {
            // We want to remove tracking information if we haven't received an update from the client for 120 minutes

            if (_playerStatus != null)
                foreach (string commander in _playerStatus.Keys)
                    if (DateTime.Now.Subtract(_playerStatus[commander].TimeStamp).TotalMinutes > 120)
                        _playerStatus.Remove(commander);

            if (_commanderStatus != null)
                foreach (string commander in _commanderStatus.Keys)
                    if (DateTime.Now.Subtract(_commanderStatus[commander].TimeStamp).TotalMinutes > 120)
                        _commanderStatus.Remove(commander);

            if (_races.Count > 0) // We remove old races after 72 hours
                foreach (Guid raceGuid in _races.Keys)
                    if (DateTime.Now.Subtract(_races[raceGuid].Start).TotalDays > 3)
                        _races.Remove(raceGuid);

            _lastStaleDataCheck = DateTime.Now;
        }

        private void Log(string log)
        {
            if (_logStream == null)
                return;

            try
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes($"{log}\n");
                _logStream.Write(buffer, 0, buffer.Length);
                _logStream.Flush();
            }
            catch { }
        }
    }
}
