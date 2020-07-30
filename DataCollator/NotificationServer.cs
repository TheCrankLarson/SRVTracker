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
        private readonly object _notificationLock = new object();
        private int _pruneCounter = 0;

        public NotificationServer(string ListenURL)
        {
            URi = ListenURL;
            _Listener = new HttpListener();

            Start();
        }

        ~NotificationServer()
        {
            Stop();
        }

        public void Start()
        {
            _Listener.Prefixes.Clear();
            _Listener.Prefixes.Add(URi);
            try
            {
                _Listener.Start();
                _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);
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
            lock (_notificationLock)
            {
                _notifications.Add(message);
            }
            _pruneCounter++;
            if (_pruneCounter>500)
                PruneNotifications();

            // Send the notification to any listening Urls
            if (_registeredNotificationUrls.Count > 0)
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
                ByteArrayContent notificationContent = new ByteArrayContent(buffer);
                foreach (string listenerUrl in _registeredNotificationUrls)
                {
                    _httpClient.PostAsync(listenerUrl, notificationContent); // Async, we don't want to wait
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

                Action action = (() => {
                    DetermineResponse(sRequest, context);
                });
                Task.Run(action);
                _Listener.BeginGetContext(new AsyncCallback(ListenerCallback), _Listener);               
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
                {
                    output.Write(buffer, 0, buffer.Length);
                }
                Context.Response.OutputStream.Flush();
                Context.Response.Close();
            }
            else
            {
                // No WebHook, so we just return a data feed (base it on IP now, but could use cookies or headers in future)
                Context.Response.KeepAlive = true;
                if (_clientNotificationPointer.ContainsKey( Context.Request.RemoteEndPoint.ToString()))
                {
                    // This is a repeat GET from a known client, so return any new data we have
                    _clientNotificationPointer[Context.Request.RemoteEndPoint.ToString()] = SendNotificationsToResponse(Context.Response, _clientNotificationPointer[Context.Request.RemoteEndPoint.ToString()]);
                 }
                else
                    // This is a new request from a client, so return all notifications we have and set up our pointer
                    _clientNotificationPointer.Add(Context.Request.RemoteEndPoint.ToString(), SendNotificationsToResponse(Context.Response, 0));
            }
        }

        private int SendNotificationsToResponse(HttpListenerResponse Response, int NotificationIndex)
        {
            int newIndex = _notifications.Count;
            string notifications = "";
            if (newIndex>NotificationIndex)
                notifications = String.Join("", _notifications.GetRange(NotificationIndex, newIndex - NotificationIndex));

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(notifications);
            Response.ContentLength64 = buffer.Length;
            Response.StatusCode = (int)HttpStatusCode.OK;
            using (Response.OutputStream)
            {
                if (buffer.Length>0)
                    Response.OutputStream.Write(buffer, 0, buffer.Length);
                Response.OutputStream.Flush();
            }
            Response.Close();
            return newIndex;
        }

        private void PruneNotifications()
        {
            _pruneCounter = 0;
            if (_notifications.Count < 1)
                return;

            if (_clientNotificationPointer.Count == 0)
            {
                // No clients, so don't keep anything
                _notifications.Clear();
                return;
            }

            // We obtain the lowest available index and remove anything before that
            int deleteBefore = _clientNotificationPointer.Values.Min();
            if (_clientNotificationPointer.Count>9000)
            {
                // We want to limit the maximum size we keep
                if (deleteBefore < 1000)
                    deleteBefore = 1000;
            }

            try
            {
                lock (_notificationLock)
                {
                    if (deleteBefore > _notifications.Count)
                        _notifications.Clear();
                    else
                        _notifications.RemoveRange(0, deleteBefore);
                    // Adjust indexes
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
            if (registrationData.ToLower().StartsWith("Subscribe:"))
            {
                // This is a subscription request (for us to send notifications to the specified URL)
                string notificationUrl = registrationData.Substring(10).Trim();
                //string responseText = "";
                if (!_registeredNotificationUrls.Contains(notificationUrl))
                {
                    _registeredNotificationUrls.Add(notificationUrl);
                    responseText = $"Successfully registered Url: {notificationUrl}";
                    return true;
                }
                else
                {
                    responseText = $"Url already registered: {notificationUrl}";
                    return true;
                }
            }
            return false;
        }



        private void Log(string Details, string Description = "")
        {

        }
    }
}
