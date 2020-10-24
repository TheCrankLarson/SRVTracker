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

        public bool IsHigherThan(string CompareVersion)
        {
            // Compare the version with the pass version, and return true if we are higher
            string[] thisVersion = version.Split('.');
            string[] compareVersion = CompareVersion.Split('.');

            int versionLength = thisVersion.Length;
            if (compareVersion.Length < thisVersion.Length)
                versionLength = compareVersion.Length;

            for (int i = 0; i < versionLength; i++)
            {
                int thisVersionSegment = Convert.ToInt32(thisVersion[i]);
                int compareVersionSegment = Convert.ToInt32(compareVersion[i]);
                if (thisVersionSegment > compareVersionSegment)
                    return true;
                if (thisVersionSegment < compareVersionSegment)
                    return false;
            }
            // Same version
            return false;
        }
    }

    public class Updater
    {
        private static string _releaseInfoUrl = "https://raw.githubusercontent.com/TheCrankLarson/SRVTracker/master/CurrentVersion.txt";
        private static string _latestReleaseVersion = "";
        private static VersionInfo _latestVersion = null;
        private static bool _runningVersionIsBeta = false;
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

        public bool RunningVersionIsBeta
        {
            get {
                if (String.IsNullOrEmpty(_latestReleaseVersion))
                    GetLatestVersion();
                return _runningVersionIsBeta;
            }
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
                {
                    webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    availableVersionInfo = webClient.DownloadString(_releaseInfoUrl);
                }
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
                            VersionInfo thisVersionInfo = new VersionInfo(releaseInfo[1], releaseInfo[2], (releaseInfo[0] == "Beta"));
                            _availableVersions.Add(thisVersionInfo);
                            if (releaseInfo[0].Equals("Release"))
                            {
                                _latestReleaseVersion = releaseInfo[1];
                                VersionInfo runningVersionInfo = new VersionInfo(ThisVersion, "", false);
                                if (runningVersionInfo.IsHigherThan(_latestReleaseVersion))
                                    _runningVersionIsBeta = true;
                            }
                        }
                    }
                } while (line != null);
            }
        }

        public string ThisVersion
        {
            get { return Application.ProductVersion; }
        }

        public bool UpdateAvailable(bool useBeta = false)
        {
            if (String.IsNullOrEmpty(_latestReleaseVersion))
                GetLatestVersion();

            if (String.IsNullOrEmpty(_latestReleaseVersion))
                return false;

            VersionInfo latestVersion = null;
            foreach (VersionInfo versionInfo in _availableVersions)
            {
                if (versionInfo.isBeta)
                {
                    if (useBeta && versionInfo.IsHigherThan(ThisVersion))
                        latestVersion = versionInfo;
                }
                else if (versionInfo.IsHigherThan(ThisVersion))
                    latestVersion = versionInfo;
            }
            if (latestVersion == null)
                return false;
            return true;
        }

        public bool DownloadUpdate(bool IncludeBeta = false)
        {
            if (!UpdateAvailable(IncludeBeta))
                return false;

            // If we get here, there is an update available.  Prompt whether to download and install.
            string latest = _latestVersion.version;
            if (_latestVersion.isBeta)
                latest += " BETA";
            DialogResult dResult = MessageBox.Show($"Installed version: {ThisVersion}{Environment.NewLine}Available version:{latest}{Environment.NewLine}{Environment.NewLine}Update now?", 
                "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dResult == DialogResult.No)
                return false;

            // Download and install update

            // To install, we copy this executable to Updater.exe, and run that to process the update

            return true;
        }
    }
}
