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
using System.Diagnostics;

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
        private DateTime _lastFinishedRaceCheck = DateTime.Now;
        private DateTime _lastCommanderStatusBuilt = DateTime.MinValue;
        private string _lastCommanderStatus = "";

        public NotificationServer(string ListenURL, bool StartDebug = false, bool VerboseDebug = false)
        {
            VerboseDebugEnabled = VerboseDebug;
            if (StartDebug)
                EnableDebug();
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

        public bool VerboseDebugEnabled { get; set; } = false;

        public void EnableDebug()
        {
            if (_logStream == null)
            {
                _logStream = File.Open("stream.log", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                return;
            }
        }

        public void DisableDebug()
        {
            if (_logStream == null)
                return;

            try
            {
                _logStream.Close();
                _logStream.Dispose();
            }
            catch { }
            _logStream = null;
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
                {
                    Log($"Updated received with blank commander: {status}", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                Log($"Error creating event: {ex.Message}");
                return;
            }

            if (_races.Count > 0)
                Task.Run(new Action(() =>
                {
                    foreach (Guid raceGuid in _races.Keys)
                    {
                        if (!_races[raceGuid].Finished)
                        {
                            _races[raceGuid].UpdateStatus(updateEvent);
                            Log($"{raceGuid}: Updated {updateEvent.Commander}", true);
                        }
                        else
                            Log($"{raceGuid}: not updated, race finished", true);
                    }
                    if (DateTime.Now.Subtract(_lastFinishedRaceCheck).TotalMinutes >= 5)
                    {
                        foreach (Guid raceGuid in _races.Keys)
                        {
                            if (!_races[raceGuid].Finished)
                                if (_races[raceGuid].CheckIfFinished())
                                    SaveRaceToDisk(raceGuid);
                        }
                        _lastFinishedRaceCheck = DateTime.Now;
                    }
                }));

            try
            {
                if (!_commanderStatus.ContainsKey(updateEvent.Commander))
                {
                    lock (_notificationLock)
                    {
                        _commanderStatus.Add(updateEvent.Commander, new EDRaceStatus(updateEvent));
                        if (_playerStatus.ContainsKey(updateEvent.Commander))
                        {
                            _playerStatus[updateEvent.Commander] = updateEvent;
                            Log($"Processed status update for commander: {updateEvent.Commander}", true);
                        }
                        else
                        {
                            _playerStatus.Add(updateEvent.Commander, updateEvent);
                            Log($"Received status update for new commander: {updateEvent.Commander}", true);
                        }
                    }
                    return;
                }

                if (_commanderStatus[updateEvent.Commander].TimeStamp > updateEvent.TimeStamp)
                {
                    Log($"Event timestamp ({updateEvent.TimeStamp}) older than existing timestamp ({_commanderStatus[updateEvent.Commander].TimeStamp}): {updateEvent.Commander}", true);
                    return;
                }

                lock (_notificationLock)
                {
                    _commanderStatus[updateEvent.Commander].UpdateStatus(updateEvent);
                    if (_playerStatus.ContainsKey(updateEvent.Commander))
                    {
                        _playerStatus[updateEvent.Commander] = updateEvent;
                        Log($"Processed status update for commander: {updateEvent.Commander}", true);
                    }
                    else
                    {
                        _playerStatus.Add(updateEvent.Commander, updateEvent);
                        Log($"Received status update for new commander: {updateEvent.Commander}", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Error processing update:{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}{status}");
                //Log($"Recreated json: {updateEvent.ToJson()}");
            }
        }

        public void ProcessNotification(string message)
        {
            // Log the notification for retrieval by clients that are polling
            try
            {
                UpdateCommanderStatus(message);
            }
            catch (Exception ex)
            {
                Log($"Error processing message:{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}{message}");
            }

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
            _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);
            try
            {
                HttpListener listener = (HttpListener)result.AsyncState;

                HttpListenerContext context = listener.EndGetContext(result);
                HttpListenerRequest request = context.Request;
                string sRequest = "";

                using (StreamReader reader = new StreamReader(request.InputStream))
                    sRequest = reader.ReadToEnd();
                string requestUri = System.Web.HttpUtility.UrlDecode(request.RawUrl.Substring(14).ToLower());
                Log($"{context.Request.RemoteEndPoint.Address}: {request.RawUrl.Substring(14)}");

                // This is really messy and will be tidied at some point
                Action action;
                if (requestUri.StartsWith("status"))
                {
                    // This is a request for all known locations/statuses of clients 
                    action = (() => {
                        SendStatus(context);
                    });
                }
                else if (requestUri.StartsWith("racestatus"))
                {
                    action = (() => {
                        SendRaceStatus(requestUri, context, sRequest);
                    });
                }
                else if (requestUri.StartsWith("startrace"))
                {
                    action = (() => {
                        StartRace(sRequest, context);
                    });
                }
                else if (requestUri.StartsWith("resurrectcommander"))
                {
                    if (requestUri.Length>19)
                    {
                        action = (() =>
                        {
                            ResurrectCommander(System.Web.HttpUtility.UrlDecode(request.RawUrl).Substring(33), context);
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
                else if (requestUri.StartsWith("getrace"))
                {
                    if (requestUri.Length > 8)
                    {
                        // Guid can be specified in the Url or in POST data.  This one has something in the Url
                        Guid raceGuid = Guid.Empty;
                        Guid.TryParse(requestUri.Substring(8), out raceGuid); 
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
                else if (requestUri.StartsWith("stoprace"))
                {
                    if (requestUri.Length > 9)
                    {
                        // Guid can be specified in the Url or in POST data.  This one has something in the Url
                        Guid raceGuid = Guid.Empty;
                        Guid.TryParse(requestUri.Substring(9), out raceGuid);
                        action = (() =>
                        {
                            StopRace(raceGuid, context);
                        });
                    }
                    else
                    {
                        // No Guid in the URL, so check request content
                        action = (() =>
                        {
                            StopRace(sRequest, context);
                        });
                    }
                }
                else if (requestUri.StartsWith("getcommanderraceevents"))
                {
                    if (requestUri.Length > 23)
                    {
                        // Guid can be specified in the Url or in POST data.  This one has something in the Url
                        try
                        {
                            string[] requestParams = System.Web.HttpUtility.UrlDecode(request.RawUrl).Substring(37).Split('/');
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
                else if (requestUri.StartsWith("getcommanderracestatus"))
                {
                    if (requestUri.Length > 23)
                    {
                        // Guid can be specified in the Url or in POST data.  This one has something in the Url
                        try
                        {
                            string[] requestParams = System.Web.HttpUtility.UrlDecode(request.RawUrl).Substring(37).Split('/');
                            Guid raceGuid = Guid.Empty;
                            Guid.TryParse(requestParams[0], out raceGuid);
                            action = (() =>
                            {
                                GetCommanderRaceStatus(raceGuid, requestParams[1], context);
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
                            GetCommanderRaceStatus(sRequest, context);
                        });
                    }
                }
                else if (requestUri.StartsWith("getcommanderracereport"))
                {
                    if (requestUri.Length > 23)
                    {
                        // Guid can be specified in the Url or in POST data.  This one has something in the Url
                        try
                        {
                            string[] requestParams = System.Web.HttpUtility.UrlDecode(request.RawUrl).Substring(37).Split('/');
                            Guid raceGuid = Guid.Empty;
                            Guid.TryParse(requestParams[0], out raceGuid);
                            action = (() =>
                            {
                                GetCommanderRaceReport(raceGuid, requestParams[1], context);
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
                            GetCommanderRaceReport(sRequest, context);
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
                Guid raceId = Guid.NewGuid();
                SaveRaceToDisk(raceId, "StartRequest", false, request);
                EDRace race = EDRace.FromString(request);
                SaveRaceToDisk(raceId, "Start", false);
                _races.Add(raceId, race);
                race.StartRace(true);
                WriteResponse(Context, raceId.ToString());
                Log($"{raceId}: Started race");
            }
            catch (Exception ex)
            {
                WriteResponse(Context,$"Error while initialising race:{Environment.NewLine}{ex}", (int)HttpStatusCode.InternalServerError);
                Log($"Failed to start new race: {ex.Message}");
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
            Guid raceGuid;
            Guid.TryParse(request, out raceGuid);
            GetRace(raceGuid, Context);
        }

        private void StopRace(Guid raceGuid, HttpListenerContext Context)
        {
            if (raceGuid != Guid.Empty && _races.ContainsKey(raceGuid))
            {
                if (!_races[raceGuid].Finished)
                {
                    Log($"{raceGuid}: race stopped by client instruction");
                    _races[raceGuid].Finished = true;
                    SaveRaceToDisk(raceGuid);
                }
                else
                    Log($"{raceGuid}: race already stopped (client requested stop)");
                WriteResponse(Context, _races[raceGuid].ToString());
            }
            else
                WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
        }

        private void StopRace(string request, HttpListenerContext Context)
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
                    Log($"{raceGuid}: Resurrected {requestParams[1]}");
                    return;
                }
            Log($"{raceGuid}: Resurrection failed for {requestParams[1]}");
            WriteResponse(Context, "Race or commander not found");  // We don't raise an error here as result is discarded client-side anyway
        }

        private void GetCommanderRaceReport(Guid raceGuid, string commander, HttpListenerContext Context)
        {
            if (raceGuid != Guid.Empty && _races.ContainsKey(raceGuid) && _races[raceGuid].Statuses.ContainsKey(commander))
                WriteResponse(Context, _races[raceGuid].Statuses[commander].RaceReport);
            else
            {
                WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
                Log($"{raceGuid}: Race report not found for {commander}");
            }
        }

        private void GetCommanderRaceReport(string request, HttpListenerContext Context)
        {
            string[] requestParams = request.Split(':');
            GetCommanderRaceReport(requestParams[0], requestParams[1], Context);
        }

        private void GetCommanderRaceReport(string raceGuid, string commander, HttpListenerContext Context)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(raceGuid, out guid);
            GetCommanderRaceReport(guid, commander, Context);
        }

        private void GetCommanderRaceStatus(Guid raceGuid, string commander, HttpListenerContext Context)
        {
            if (raceGuid != Guid.Empty && _races.ContainsKey(raceGuid) && _races[raceGuid].Statuses.ContainsKey(commander))
                WriteResponse(Context, _races[raceGuid].Statuses[commander].ToJson());
            else
            {
                WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
                Log($"{raceGuid}: Race status not found for {commander}");
            }
        }

        private void GetCommanderRaceStatus(string request, HttpListenerContext Context)
        {
            string[] requestParams = request.Split(':');
            GetCommanderRaceStatus(requestParams[0], requestParams[1], Context);
        }

        private void GetCommanderRaceStatus(string raceGuid, string commander, HttpListenerContext Context)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(raceGuid, out guid);
            GetCommanderRaceStatus(guid, commander, Context);
        }

        private void GetCommanderRaceEvents(Guid raceGuid, string commander, HttpListenerContext Context)
        {
            if (raceGuid != Guid.Empty && _races.ContainsKey(raceGuid))
            {
                List<EDEvent> eventHistory = _races[raceGuid].GetCommanderEventHistory(commander);
                if (eventHistory != null)
                {
                    WriteResponse(Context, JsonSerializer.Serialize(_races[raceGuid].GetCommanderEventHistory(commander)));
                    return;
                }
            }
            WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
            Log($"{raceGuid}: Event history not found for {commander}");
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
            {
                WriteResponse(Context, _races[raceGuid].ExportRaceStatistics());
                Log($"{raceGuid}: Race statistics sent");
            }
            else
            {
                WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
                Log("Race statistics for unknown race requested");
            }
            
            if (DateTime.Now.Subtract(_lastStaleDataCheck).TotalMinutes > 120)
                Task.Run(new Action(()=> { ClearStaleData(); }));
        }

        private void SendStatus(HttpListenerContext Context)
        {
            // Send Commander status


            // Check if a client Id was specified
            if (Context.Request.RawUrl.Length > 20)
            {
                string clientId = System.Web.HttpUtility.UrlDecode(Context.Request.RawUrl).Substring(21).ToLower();
                if (_playerStatus.ContainsKey(clientId))
                {
                    Log($"Player status requested: {clientId}");
                    WriteResponse(Context, _playerStatus[clientId].ToJson());
                }
                else
                {
                    //Log($"Status requested for invalid client: {clientId}");
                    WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
                }
                return;
            }

            if (DateTime.Now.Subtract(_lastCommanderStatusBuilt).TotalMilliseconds > 750)
            {
                StringBuilder status = new StringBuilder();
                foreach (string id in _playerStatus.Keys)
                    status.AppendLine(_playerStatus[id].ToJson());
                Log("All player status generated");
                _lastCommanderStatus = status.ToString();
                _lastCommanderStatusBuilt = DateTime.Now;
            }
            else
                Log("All player status returned from cache");

            WriteResponse(Context, _lastCommanderStatus);
            if (DateTime.Now.Subtract(_lastStaleDataCheck).TotalMinutes > 120)
                ClearStaleData();
        }

        private void ClearStaleData()
        {
            // We want to remove tracking information if we haven't received an update from the client for 120 minutes
            List<string> playersToRemove = new List<string>();
            if (_playerStatus != null)
            {
                foreach (string commander in _playerStatus.Keys)
                    if (DateTime.Now.Subtract(_playerStatus[commander].TimeStamp).TotalMinutes > 120)
                        playersToRemove.Add(commander);
                foreach(string commander in playersToRemove)
                    {
                        _playerStatus.Remove(commander);
                        Log($"{commander}: deleted tracking data as last update over 2 hours ago", true);
                    }
                playersToRemove = new List<string>();
            }

            if (_commanderStatus != null)
            {
                foreach (string commander in _commanderStatus.Keys)
                    if (DateTime.Now.Subtract(_commanderStatus[commander].TimeStamp).TotalMinutes > 120)
                        playersToRemove.Add(commander);
                foreach (string commander in playersToRemove)
                {
                    _commanderStatus.Remove(commander);
                    Log($"{commander}: deleted race status data as last update over 2 hours ago", true);
                }
            }

            if (_races.Count > 0) // We remove old races after 72 hours
                foreach (Guid raceGuid in _races.Keys)
                {
                    TimeSpan timeSinceStart = DateTime.Now.Subtract(_races[raceGuid].Start);
                    if (timeSinceStart.TotalDays > 3)
                    {
                        _races.Remove(raceGuid);
                        Log($"{raceGuid}: deleted race as older than three days");
                    }
                    else if ( !_races[raceGuid].Finished && (_races[raceGuid].Start != DateTime.MinValue) )
                    {
                        if (timeSinceStart.TotalHours > 24)
                        {
                            _races[raceGuid].Finished = true;
                            Log($"{raceGuid}: race marked as finished due to 24 hours since start");
                            SaveRaceToDisk(raceGuid);
                        }
                        else
                        {
                            // Check each racer status to see if race is finished
                            bool allFinished = true;
                            foreach (EDRaceStatus status in _races[raceGuid].Statuses.Values)
                                if (!status.Finished && !status.Eliminated)
                                {
                                    allFinished = false;
                                    break;
                                }
                            if (allFinished)
                            {
                                _races[raceGuid].Finished = true;
                                Log($"{raceGuid}: finished - all participants finished or eliminated");
                                SaveRaceToDisk(raceGuid);
                            }
                        }
                    }
                }

            _lastStaleDataCheck = DateTime.Now;
        }

        private string GetRaceSaveFolder(Guid raceGuid)
        {
            if (!Directory.Exists("Races"))
                return "";

            string raceDataPath = $"Races\\{raceGuid}";
            if (!Directory.Exists(raceDataPath))
            {
                try
                {
                    Directory.CreateDirectory(raceDataPath);
                    return raceDataPath;
                }
                catch { }
            }
            else
                return raceDataPath;
            return "";
        }

        private void SaveRaceToDisk(Guid raceGuid, string fileExtension = "Summary", bool includeEventData = true, string raceData = "")
        {
            string raceDataPath = GetRaceSaveFolder(raceGuid);
            if (String.IsNullOrEmpty(raceDataPath))
                return;

            try
            {
                if (String.IsNullOrEmpty(raceData))
                    File.WriteAllText($"{raceDataPath}\\Race.{fileExtension}", _races[raceGuid].ToString());
                else
                    File.WriteAllText($"{raceDataPath}\\Race.{fileExtension}", raceData);

                if (includeEventData)
                    foreach (string commander in _races[raceGuid].Contestants)
                        File.WriteAllText($"{raceDataPath}\\{commander}.Tracking", JsonSerializer.Serialize(_races[raceGuid].GetCommanderEventHistory(commander)));
            }
            catch { }
        }

        private int _flushCount = 0;
        private void Log(string log, bool Verbose = false)
        {
            if ( (_logStream == null) || (Verbose && !VerboseDebugEnabled) )
                return;

            try
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes($"{DateTime.Now:HH:mm:ss} {log}{Environment.NewLine}");
                _logStream.Write(buffer, 0, buffer.Length);
                _flushCount++;
                if (_flushCount > 50)
                {
                    _logStream.Flush();
                    _flushCount = 0;
                }
            }
            catch { }
        }
    }
}
