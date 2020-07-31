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

namespace DataCollator
{
    class NotificationServer
    {
        private HttpListener _Listener = null;
        private int _ListenPort = 11938;
        private List<HttpListenerResponse> _responses = new List<HttpListenerResponse>();
        private List<string> _registeredNotificationUrls = new List<string>();
        private static HttpClient _httpClient = new HttpClient();
        private List<String> _notifications = new List<string>(10000); // List of all notifications, pruned as each client gets up to date
        private Dictionary<string, int> _clientNotificationPointer = new Dictionary<string, int>(); // Pointer into the notification table for each client
        private Dictionary<string, DateTime> _clientLastRequestTime = new Dictionary<string, DateTime>();
        private Dictionary<string, string> _playerStatus = new Dictionary<string, string>(); //  Store last known status (with coordinates) of client
        private readonly object _notificationLock = new object();
        private int _pruneCounter = 0;
        private FileStream _logStream = null;

        public NotificationServer(string ListenURL, bool EnableDebug = false)
        {
            if (EnableDebug)
                _logStream = File.Open("stream.log", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            URi = ListenURL;
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

        public void SendNotification(string message)
        {
            // Log the notification for retrieval by clients that are polling
            string clientId = null;
            try
            {
                // We store clientId in lower case (for case insensitive matching), but return the original (as we store it in the status)
                clientId = message.Substring(0, message.IndexOf(',')).ToLower();
            }
            catch { }
            lock (_notificationLock)
            {
                _notifications.Add(message);
                if (!String.IsNullOrEmpty(clientId))
                    if (_playerStatus.ContainsKey(clientId))
                        _playerStatus[clientId] = message;
                    else
                        _playerStatus.Add(clientId, message);
            }
            _pruneCounter++;
            if (_pruneCounter>500)
                PruneNotifications();

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

                Action action;
                if (request.RawUrl.ToLower().StartsWith("/datacollator/status"))
                {
                    // This is a request for all known locations/statuses of clients 
                    action = (() => {
                        SendStatus(context);
                    });
                }
                else
                    action = (() => {
                        DetermineResponse(sRequest, context);
                    });
                Task.Run(action);
                _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);               
            }
            catch { }
        }

        private void SendStatus(HttpListenerContext Context)
        {
            // Send all known client locations
            // Tracking info is: Client Id,timestamp,latitude,longitude,altitude,heading,planet radius,flags

            StringBuilder status = new StringBuilder();
            status.AppendLine("Client Id,timestamp(ticks),latitude,longitude,altitude,heading,planet radius,flags");

            // Check if a client Id was specified
            string clientId = "";
            try
            {
                clientId = System.Web.HttpUtility.UrlDecode(Context.Request.RawUrl).Substring("/DataCollator/status/".Length).ToLower();
            }
            catch { }
            if (String.IsNullOrEmpty(clientId))
            {
                Log("All player status requested");
                foreach (string id in _playerStatus.Keys)
                    status.AppendLine(_playerStatus[id]);
            }
            else if (_playerStatus.ContainsKey(clientId))
            {
                Log($"Player status requested: {clientId}");
                status.AppendLine(_playerStatus[clientId]);
            }
            else
                Log($"Status requested for invalid client: {clientId}");

            try
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(status.ToString());
                Context.Response.ContentLength64 = buffer.Length;
                Context.Response.StatusCode = (int)HttpStatusCode.OK;

                using (Stream output = Context.Response.OutputStream)
                    output.Write(buffer, 0, buffer.Length);
                Context.Response.OutputStream.Flush();
                Context.Response.KeepAlive = true;
                Context.Response.Close();
            }
            catch { }
        }

        private void DetermineResponse(string Request, HttpListenerContext Context)
        {
            string responseText = "";
            Context.Response.ContentType = "text/plain";
            

            if (isValidClientRegistration(Request, out responseText))
            {
                // Client has registered a listener Url.  We just respond with confirmation
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseText);
                Context.Response.ContentLength64 = buffer.Length;
                Context.Response.StatusCode = (int)HttpStatusCode.OK;

                using (Stream output = Context.Response.OutputStream)
                    output.Write(buffer, 0, buffer.Length);
                Context.Response.OutputStream.Flush();

                Context.Response.Close();
            }
            else
            {
                // No WebHook, so we just return a data feed (base it on IP now, but could use cookies or headers in future)
                Context.Response.KeepAlive = true;
                SendNotificationsToResponse(Context.Response, Context.Request.RemoteEndPoint.ToString());
            }
        }

        private void SendNotificationsToResponse(HttpListenerResponse Response, string ClientId)
        {
            // Build string of all events that we need to transmit
            if (!_clientNotificationPointer.ContainsKey(ClientId))
            {
                _clientNotificationPointer.Add(ClientId, 0); // New client, set index to 0
                Log($"New streaming client detected: {ClientId}");
            }

            if (!_clientLastRequestTime.ContainsKey(ClientId))
                _clientLastRequestTime.Add(ClientId, DateTime.Now);
            else
                _clientLastRequestTime[ClientId] = DateTime.Now;

            int newIndex = _notifications.Count;
            string notifications = "";
            if (newIndex > _clientNotificationPointer[ClientId])
            {
                notifications = String.Join("", _notifications.GetRange(_clientNotificationPointer[ClientId], newIndex - _clientNotificationPointer[ClientId]));
                Log($"{ClientId} - Sending notifications range {_clientNotificationPointer[ClientId]} to {newIndex}");
            }
            _clientNotificationPointer[ClientId] = newIndex; // Set the client index pointer
            

            // Send the events to the response stream
            try
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(notifications);
                Response.ContentLength64 = buffer.Length;
                Response.StatusCode = (int)HttpStatusCode.OK;
                using (Response.OutputStream)
                {
                    if (buffer.Length > 0)
                        Response.OutputStream.Write(buffer, 0, buffer.Length);
                    Response.OutputStream.Flush();
                }
                Response.Close();
            }
            catch { }            
        }

        private void PruneClients()
        {
            // Process our client list and get rid of any that haven't requested data in the last 60 minutes

            if (_clientNotificationPointer.Count == 0)
                return;

            DateTime removeBefore = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
            lock (_notificationLock)
            {
                foreach (string clientId in _clientLastRequestTime.Keys)
                    if (_clientLastRequestTime[clientId] < removeBefore)
                        _clientNotificationPointer.Remove(clientId);
            }
        }

        private void PruneNotifications()
        {
            _pruneCounter = 0;
            if (_notifications.Count < 1)
                return;

            if (_clientNotificationPointer.Count == 0)
            {
                // No clients, so don't keep anything
                Log("No active clients, clearing notifications cache");
                _notifications.Clear();
                return;
            }
            PruneClients();

            // We obtain the lowest available index and remove anything before that
            int deleteBefore = _clientNotificationPointer.Values.Min();
            Log($"Minimum client index is {deleteBefore}");
            if (_clientNotificationPointer.Count>9000)
            {
                // We want to limit the maximum size we keep
                if (deleteBefore < 1000)
                    deleteBefore = 1000;
            }
            Log($"Will delete all notifications below index {deleteBefore}");

            try
            {
                lock (_notificationLock)
                {
                    try
                    {
                        if (deleteBefore >= _notifications.Count)
                            _notifications.Clear();
                        else
                            _notifications.RemoveRange(0, deleteBefore);
                    }
                    catch (Exception ex)
                    {
                        Log($"Error clearing notifications cache: {ex.Message}");
                        return; // No need to adjust indexes if we couldn't clear the cache
                    }
                    // Adjust indexes
                    Log($"Reducing client indexes by {deleteBefore}");
                    foreach (string client in _clientNotificationPointer.Keys)
                    {
                        if (_clientNotificationPointer[client] > deleteBefore)
                            _clientNotificationPointer[client] = _clientNotificationPointer[client] - deleteBefore;
                        else
                            _clientNotificationPointer[client] = 0;
                    }
                }
            }
            catch { }
        }

        private bool isValidClientRegistration(string registrationData, out string responseText)
        {
            responseText = "";
            if (registrationData.ToLower().StartsWith("subscribe:"))
            {
                // This is a subscription request (for us to send notifications to the specified URL)
                string notificationUrl = registrationData.Substring(10).Trim();

                if (notificationUrl.ToLower().StartsWith("http")) // Only accept http/s URLs
                {
                    if (!_registeredNotificationUrls.Contains(notificationUrl))
                    {
                        _registeredNotificationUrls.Add(notificationUrl);
                        responseText = $"Successfully registered Url: {notificationUrl}";
                        Log($"Subscription Url registered: {notificationUrl}");
                    }
                    else
                        responseText = $"Url already registered: {notificationUrl}";
                    Log($"Subscription Url already registered: {notificationUrl}");
                }
                else
                {
                    responseText = $"Invalid Url: {notificationUrl}";
                    Log($"Subscription Url invalid: {notificationUrl}");
                }
                return true;
            }
            if (registrationData.Equals("LOCATIONS"))
            {
                // This is a request for all known current locations

            }
            return false;
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
