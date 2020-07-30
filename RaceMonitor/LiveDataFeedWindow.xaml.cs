using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SRVTracker;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RaceMonitor
{
    /// <summary>
    /// Interaction logic for LiveDataFeedWindow.xaml
    /// </summary>
    public partial class LiveDataFeedWindow : Window
    {
        private string _eventUrl = "";
        private bool _retrieveEvents = true;

        public LiveDataFeedWindow(string eventUrl)
        {
            InitializeComponent();
            _eventUrl = eventUrl;
            Action action = new Action(() => { ProcessEvents(); });
            Task.Run(action);
        }

        private string RetrieveEvents()
        {
            string events = "";
            if (!_retrieveEvents || String.IsNullOrEmpty(_eventUrl))
                return events;

            try
            {
                WebRequest request = WebRequest.Create(_eventUrl);
                WebResponse response = request.GetResponse();
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    // Read the events
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        events = reader.ReadToEnd();
                    }
                    response.Close();
                }
            }
            catch { }
            return events;
        }

        private void ProcessEvents()
        {
            if (_retrieveEvents)
            {
                // Retrieve and process the events
                string events = RetrieveEvents();
                if (!String.IsNullOrEmpty(events))
                {
                    foreach (string ev in events.Split('\n'))
                    {

                    }
                }
            }
            else
                Thread.Sleep(100);

            Action action = new Action(() => { ProcessEvents(); });
            Task.Run(action);
        }
    }
}
