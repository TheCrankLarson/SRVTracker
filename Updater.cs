using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Windows.Shapes;

namespace SRVTracker
{
    class VersionInfo
    {
        public string version;
        public string downloadUrl;
        public bool isBeta;

        public VersionInfo(string Version, string DownloadUrl, bool IsBeta)
        {
            version = Version;
            downloadUrl = DownloadUrl;
            isBeta = IsBeta;
        }
    }

    public class Updater
    {
        private static string _releaseInfoUrl = "https://raw.githubusercontent.com/TheCrankLarson/SRVTracker/master/CurrentVersion.txt";
        private static string _latestReleaseVersion = "";
        private static string _latestBetaVersion = "";
        private static List<VersionInfo> _availableVersions = new List<VersionInfo>();

        public Updater()
        {

        }

        public void ProcessUpdate()
        {
            string updateFile = $"{Application.ProductName}.update";
            if (!File.Exists(updateFile))
                return;

            // We have an update info file
            string updateInfo = File.ReadAllText(updateFile);
            ProcessUpdateInfo(updateInfo);
        }

        private void WriteUpdateFile(string updateInfo)
        {
            File.WriteAllText($"{Application.ProductName}.update", updateInfo);
        }

        private void ProcessUpdateInfo(string updateInfo)
        {
            if (updateInfo.Equals("Rename"))
            {
                // We are in the final part of the update, and need to rename the updated application to the original name
                string appFileName = $"{Application.ProductName}.exe";
            }
        }

        private void GetLatestVersion()
        {
            string availableVersionInfo = "";
            try
            {
                using (WebClient webClient = new WebClient())
                    availableVersionInfo = webClient.DownloadString(_releaseInfoUrl);
            }
            catch { }
            if (String.IsNullOrEmpty(availableVersionInfo))
                return;

            using (StringReader reader = new StringReader(availableVersionInfo))
            {
                string line;
                do
                {
                    line = reader.ReadLine();
                    if (!String.IsNullOrEmpty(line))
                    {
                        // Release 1.1.0 https://github.com/TheCrankLarson/SRVTracker/releases/download/1.1.0/SRVTracker.1.1.0.zip
                        string[] releaseInfo = line.Split(' ');
                        if (releaseInfo.Length == 3)
                        {
                            _availableVersions.Add(new VersionInfo(releaseInfo[1], releaseInfo[2], (releaseInfo[0] == "Beta")));
                            if (releaseInfo[0].Equals("Release"))
                                _latestReleaseVersion = releaseInfo[1];
                            else if (releaseInfo[0].Equals("Beta"))
                                _latestBetaVersion = releaseInfo[1];
                        }
                    }
                } while (line != null);
            }
        }

        public string ThisVersion
        {
            get { return Application.ProductVersion; }
        }

        public bool UpdateAvailable()
        {
            if (String.IsNullOrEmpty(_latestReleaseVersion))
                GetLatestVersion();

            if (String.IsNullOrEmpty(_latestReleaseVersion))
                return false;

            string[] currentVersion = ThisVersion.Split('.');
            string[] latestVersion = _latestReleaseVersion.Split('.');

            int versionLength = currentVersion.Length;
            if (latestVersion.Length < currentVersion.Length)
                versionLength = latestVersion.Length;

            for (int i = 0; i < versionLength; i++)
            {
                int current = Convert.ToInt32(currentVersion[i]);
                int updated = Convert.ToInt32(latestVersion[i]);
                if (current < updated)
                    return true;
                if (current > updated)
                    return false;
            }
            // Same version
            return false;
        }

        public bool DownloadUpdate()
        {
            if (!UpdateAvailable())
                return false;

            // If we get here, there is an update available.  Prompt whether to download and install.
            DialogResult dResult = MessageBox.Show($"Installed version: {ThisVersion}{Environment.NewLine}Available version:{_latestReleaseVersion}{Environment.NewLine}Update now?", 
                "Updated available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dResult == DialogResult.No)
                return false;

            // Download and install update

            // To install, we copy this executable to Updater.exe, and run that to process the update

            return true;
        }
    }
}
