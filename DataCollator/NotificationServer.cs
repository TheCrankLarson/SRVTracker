using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using EDTracking;
using System.Text.Json;

namespace DataCollator
{
    class NotificationServer
    {
        private HttpListener _Listener = null;
        private HttpListener _webListener = null;
        private int _ListenPort = 11938;
        private List<string> _registeredNotificationUrls = new List<string>();
        private static HttpClient _httpClient = new HttpClient();
        private Dictionary<string, EDEvent> _playerStatus = new Dictionary<string, EDEvent>(); //  Store last known status (with coordinates) of client
        private readonly object _notificationLock = new object();
        private readonly object _logWriteLock = new object();
        private FileStream _logStream = null;
        private Dictionary<Guid, EDRace> _races;
        private DateTime _lastStaleDataCheck = DateTime.UtcNow;
        private DateTime _lastFinishedRaceCheck = DateTime.UtcNow;
        private DateTime _lastAllCommanderStatusBuilt = DateTime.MinValue;
        private string _lastAllCommanderStatus = "";
        private CommanderRegistration _commanderRegistration = new CommanderRegistration();
        private Mischief.ImpMaster _mischiefMaker = new Mischief.ImpMaster();

        public string WebURL { get; set; } = null;

        public NotificationServer(string ListenURL, string WebUrl = "", bool StartDebug = false, bool VerboseDebug = false)
        {
            VerboseDebugEnabled = VerboseDebug;
            if (StartDebug)
                EnableDebug();
            URi = ListenURL;
            _races = new Dictionary<Guid, EDRace>();
            _Listener = new HttpListener();
            WebURL = WebUrl;

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
                _logStream = File.Open("stream.log", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
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
            if (!String.IsNullOrEmpty(WebURL))
                _Listener.Prefixes.Add(WebURL);
            try
            {
                _Listener.Start();
                for (int i=0; i<5; i++)
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
                string commanderName = _commanderRegistration.CommanderName(updateEvent.Commander);
                if (String.IsNullOrEmpty(commanderName))
                {
                    Log($"Unregistered commander ignored: {updateEvent.Commander}", true);
                    return;
                }
                updateEvent.Commander = commanderName;
            }
            catch (Exception ex)
            {
                LogError($"Error creating event: {ex.Message}");
                return;
            }

            if (_races.Count > 0)
                Task.Run(new Action(() =>
                {
                    foreach (Guid raceGuid in _races.Keys)
                    {
                        if (!_races[raceGuid].Finished)
                        {
                            try
                            {
                                _races[raceGuid].UpdateStatus(updateEvent);
                                Log($"{raceGuid}: Updated {updateEvent.Commander}", true);
                            }
                            catch (Exception ex)
                            {
                                LogError($"{raceGuid}: Unexpected error on update: {ex}");
                            }
                        }
                        else
                            Log($"{raceGuid}: not updated, race finished", true);
                    }
                    if (DateTime.UtcNow.Subtract(_lastFinishedRaceCheck).TotalMinutes >= 5)
                    {
                        foreach (Guid raceGuid in _races.Keys)
                        {
                            if (!_races[raceGuid].Finished)
                                if (_races[raceGuid].CheckIfFinished())
                                    SaveRaceToDisk(raceGuid);
                        }
                        _lastFinishedRaceCheck = DateTime.UtcNow;
                    }
                }));

            try
            {
                lock (_notificationLock)
                {
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
                LogError($"Error processing update:{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}{status}");
            }
        }

        public void ProcessNotification(string message)
        {
            // Process received event
            try
            {
                UpdateCommanderStatus(message);
            }
            catch (Exception ex)
            {
                LogError($"Error processing message:{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}{message}");
                return;
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
            try
            {
                _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);
            }
            catch { }
            
            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);
            
            try
            {

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
                    action = (() => { SendStatus(context); });
                }
                else if (requestUri.StartsWith("trackedcommanders"))
                {
                    action = (() => { SendAllStatus(context); });
                }
                else if (requestUri.StartsWith("racestatus"))
                {
                    action = (() => { SendRaceStatus(requestUri, context, sRequest); });
                }
                else if (requestUri.StartsWith("racemonitoring/"))
                {
                    action = (() => { ReturnWebResource(requestUri, context); });
                }
                else if (requestUri.StartsWith("startrace"))
                {
                    action = (() =>{ StartRace(sRequest, context); });
                }
                else if (requestUri.StartsWith("resurrectcommander"))
                {
                    if (requestUri.Length > 19)
                        action = (() => { ResurrectCommander(System.Web.HttpUtility.UrlDecode(request.RawUrl).Substring(33), context); });
                    else
                        action = (() => { WriteErrorResponse(context.Response, HttpStatusCode.NotFound); });
                }
                else if (requestUri.StartsWith("getactiveraces"))
                {
                    action = (() =>
                    {
                        try
                        {
                            StringBuilder activeRaces = new StringBuilder();
                            foreach (Guid guid in _races.Keys)
                                if (!_races[guid].Finished)
                                    activeRaces.AppendLine($"{guid},{_races[guid].Name}");
                            WriteResponse(context, activeRaces.ToString());
                        }
                        catch (Exception ex)
                        {
                            LogError($"GetActiveRaces: {ex.Message}");
                            WriteErrorResponse(context.Response, HttpStatusCode.InternalServerError);
                        }
                    });
                }
                else if (requestUri.StartsWith("getrace"))
                {
                    if (requestUri.Length > 8)
                    {
                        // Guid can be specified in the Url or in POST data.  This one has something in the Url
                        Guid raceGuid = Guid.Empty;
                        Guid.TryParse(requestUri.Substring(8), out raceGuid);
                        action = (() => { GetRace(raceGuid, context); });
                    }
                    else
                    {
                        // No Guid in the URL, so check request content
                        action = (() => { GetRace(sRequest, context); });
                    }
                }
                else if (requestUri.StartsWith("stoprace"))
                {
                    if (requestUri.Length > 9)
                    {
                        // Guid can be specified in the Url or in POST data.  This one has something in the Url
                        Guid raceGuid = Guid.Empty;
                        Guid.TryParse(requestUri.Substring(9), out raceGuid);
                        action = (() => { StopRace(raceGuid, context); });
                    }
                    else
                    {
                        // No Guid in the URL, so check request content
                        action = (() => { StopRace(sRequest, context); });
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
                            action = (() => { GetCommanderRaceEvents(raceGuid, requestParams[1], context); });
                        }
                        catch
                        {
                            action = (() => { WriteErrorResponse(context.Response, HttpStatusCode.NotFound); });
                        }
                    }
                    else
                        action = (() => { GetCommanderRaceEvents(sRequest, context); });
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
                            action = (() => { GetCommanderRaceStatus(raceGuid, requestParams[1], context); });
                        }
                        catch
                        {
                            action = (() => { WriteErrorResponse(context.Response, HttpStatusCode.NotFound); });
                        }
                    }
                    else
                    {
                        action = (() => { GetCommanderRaceStatus(sRequest, context); });
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
                            action = (() => { GetCommanderRaceReport(raceGuid, requestParams[1], context); });
                        }
                        catch
                        {
                            action = (() => { WriteErrorResponse(context.Response, HttpStatusCode.NotFound); });
                        }
                    }
                    else
                        action = (() => { GetCommanderRaceReport(sRequest, context); });
                }
                else if (requestUri.StartsWith("registercommander"))
                {
                    action = (() => { WriteResponse(context, _commanderRegistration.RegisterCommander(sRequest)); });
                }
                else if (requestUri.StartsWith("renamecommander"))
                {
                    string[] renameArgs = sRequest.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    if (renameArgs.Length == 2)
                        action = (() => { WriteResponse(context, _commanderRegistration.UpdateCommanderName(renameArgs[0], renameArgs[1])); });
                    else
                        action = (() => { WriteErrorResponse(context.Response, HttpStatusCode.BadRequest); });
                }
                else
                    action = (() => { WriteResponse(context, $"{Application.ProductName} v{Application.ProductVersion}"); });

                action();
            }
            catch (Exception ex)
            {
                LogError($"ListenerCallback: {ex}");
                Log($"Requested Url: {context.Request.RawUrl}");
            }
           
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
                WriteResponse(httpResponse, buffer, returnStatusCode);
            }
            catch { }
        }

        private void WriteResponse(HttpListenerResponse httpResponse, byte[] response, int returnStatusCode = (int)HttpStatusCode.OK)
        {
            try
            {
                httpResponse.ContentLength64 = response.Length;
                httpResponse.StatusCode = returnStatusCode;

                //using (Stream output = httpResponse.OutputStream)
                httpResponse.OutputStream.Write(response, 0, response.Length);
                httpResponse.OutputStream.Flush();
                httpResponse.Close();
            }
            catch { }
        }

        private void WriteResponse(HttpListenerContext Context, string response, int returnStatusCode = (int)HttpStatusCode.OK)
        {
            WriteResponse(Context.Response, response, returnStatusCode);
        }

        private void WriteResponse(HttpListenerContext Context, byte[] response, int returnStatusCode = (int)HttpStatusCode.OK)
        {
            WriteResponse(Context.Response, response, returnStatusCode);
        }

        private void WriteFileResponse(HttpListenerContext Context, string Filename)
        {
            if (!File.Exists(Filename))
                WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
            else
            {
                string ext = Filename.Substring(Filename.Length - 3).ToLower();
                if (ext.Equals("png") || ext.Equals("jpg") || ext.Equals("ttf") )
                {
                    WriteResponse(Context, File.ReadAllBytes(Filename));
                }
                else
                    WriteResponse(Context, File.ReadAllText(Filename));
            }
        }

        private void ReturnWebResource(string request, HttpListenerContext Context)
        {
            try
            {
                string requestedFile = request.Substring(request.LastIndexOf("racemonitoring/") + 15);
                WriteFileResponse(Context, $"RaceMonitoring/{requestedFile}");
            }
            catch
            {
                WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
            }
        }

        private void StartRace(string request, HttpListenerContext Context)
        {
            // Client has requested to start race monitoring.  The request should be an EDRace json.  We return a Guid

            try
            {
                Guid raceId = Guid.NewGuid();
                SaveRaceToDisk(raceId, "StartRequest", false, request);
                EDRace race = EDRace.FromString(request);
                race.Log = Log;
                _races.Add(raceId, race);
                SaveRaceToDisk(raceId, "Start", false);
                race.StartRace(true);
                WriteResponse(Context, raceId.ToString());
                Log($"{raceId}: Started race");
            }
            catch (Exception ex)
            {
                WriteResponse(Context,$"Error while initialising race:{Environment.NewLine}{ex}", (int)HttpStatusCode.InternalServerError);
                LogError($"Failed to start new race: {ex.Message}");
            }
        }

        private void GetRace(Guid raceGuid, HttpListenerContext Context)
        {
            if (raceGuid != Guid.Empty && _races.ContainsKey(raceGuid))
                WriteResponse(Context, _races[raceGuid].CachedToString());
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
            if (_races.ContainsKey(raceGuid) && _races[raceGuid].Statuses.ContainsKey(commander))
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
            if (requestUri.Length > 11)
            {
                // Guid can be specified in the Url or in POST data.  This one has something in the Url
                Guid.TryParse(requestUri.Substring(11), out raceGuid);
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
                Log($"Race statistics for unknown race requested: {raceGuid}");
            }
            
            //if (DateTime.UtcNow.Subtract(_lastStaleDataCheck).TotalMinutes > 120)
            //    Task.Run(new Action(()=> { ClearStaleData(); }));
        }

        private void SendStatus(HttpListenerContext Context)
        {
            // Send Commander status
            if (DateTime.UtcNow.Subtract(_lastStaleDataCheck).TotalSeconds > 60)
                ClearStaleData();

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

            WriteErrorResponse(Context.Response, HttpStatusCode.NotFound);
        }

        private void SendAllStatus(HttpListenerContext Context)
        {
            if (DateTime.UtcNow.Subtract(_lastAllCommanderStatusBuilt).TotalMilliseconds > 500)
            {
                String status;
                lock (_notificationLock)
                {
                    status = JsonSerializer.Serialize(_playerStatus);
                }
                Log("All player status generated");
                _lastAllCommanderStatus = status;
                _lastAllCommanderStatusBuilt = DateTime.UtcNow;
            }
            else
                Log("All player status returned from cache");

            WriteResponse(Context, _lastAllCommanderStatus);
        }

        private void ClearStaleData()
        {
            // We want to remove tracking information if we haven't received an update from the client for 60 seconds
            Log("Checking for stale data to purge", true);
            List<string> playersToRemove = new List<string>();
            if (_playerStatus != null)
            {
                foreach (string commander in _playerStatus.Keys)
                    if (DateTime.UtcNow.Subtract(_playerStatus[commander].TimeStamp).TotalSeconds > 60)
                        playersToRemove.Add(commander);
                if (playersToRemove.Count > 0)
                {
                    lock (_notificationLock)
                    {
                        foreach (string commander in playersToRemove)
                        {
                            _playerStatus.Remove(commander);
                            Log($"{commander}: deleted tracking data as last update over 1 minute ago", true);
                        }
                    }
                    playersToRemove = new List<string>();
                }
            }

            if (_races.Count > 0) // We remove old races after 72 hours
            {
                List<Guid> racesToRemove = new List<Guid>();
                foreach (Guid raceGuid in _races.Keys)
                {
                    TimeSpan timeSinceStart = DateTime.UtcNow.Subtract(_races[raceGuid].Start);
                    if (timeSinceStart.TotalDays > 3)
                    {
                        racesToRemove.Add(raceGuid);
                        Log($"{raceGuid}: deleting race as older than three days");
                    }
                    else if (!_races[raceGuid].Finished && (_races[raceGuid].Start > DateTime.MinValue))
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

                if (racesToRemove.Count > 0)
                    foreach (Guid raceToRemove in racesToRemove)
                        _races.Remove(raceToRemove);
            }

            _lastStaleDataCheck = DateTime.UtcNow;
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
        private void Log(string log, bool Verbose)
        {
            if ( (_logStream == null) || (Verbose && !VerboseDebugEnabled) )
                return;

            lock (_logWriteLock)
            {
                try
                {
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes($"{DateTime.UtcNow:HH:mm:ss} {log}{Environment.NewLine}");
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

        public void Log(string log)
        {
            Log(log, false);
        }

        private void LogError(string log)
        {
            // If we get an unexpected error, we always want to log it
            bool loggingOn = _logStream != null;
            if (!loggingOn)
                EnableDebug();

            Log(log);
            if (!loggingOn)
                DisableDebug();
        }
    }
}
