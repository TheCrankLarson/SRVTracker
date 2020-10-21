using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace SRVTracker
{
    public class Updater
    {
        static string _releaseUrl = "https://raw.githubusercontent.com/TheCrankLarson/SRVTracker/master/CurrentVersion.txt";
        private string _latestOnlineVersion = "";

        public Updater()
        {
            GetLatestVersion();
        }

        private void GetLatestVersion()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                    _latestOnlineVersion = webClient.DownloadString(_releaseUrl);
            }
            catch
            {
                return;
            }
        }

        public string CurrentVersion
        {
            get { return Application.ProductVersion; }
        }

        public bool UpdateAvailable()
        {
            if (String.IsNullOrEmpty(_latestOnlineVersion))
                GetLatestVersion();

            if (String.IsNullOrEmpty(_latestOnlineVersion))
                return false;

            string[] currentVersion = CurrentVersion.Split('.');
            string[] latestVersion = _latestOnlineVersion.Split('.');
            for (int i = 0; i < currentVersion.Length; i++)
            {
                int current = Convert.ToInt32(currentVersion[i]);
                int updated = Convert.ToInt32(latestVersion[i]);
                if (current < updated)
                    return true;
                if (current > updated)
                    return false;
            }
            return false;
        }
    }
}
