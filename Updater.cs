using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace SRVTracker
{
    public class VersionInfo
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

        public override string ToString()
        {
            if (isBeta)
                return $"Beta {version} {downloadUrl}";
            return $"Release {version} {downloadUrl}";
        }

        public static VersionInfo FromString(string versionInfo)
        {
            string[] infoSegments = versionInfo.Split(' ');
            if (infoSegments.Length != 3)
                return null;
            return new VersionInfo(infoSegments[1], infoSegments[2], infoSegments[0].Equals("Beta"));
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

        public VersionInfo LatestAvailableVersion()
        {
            if (_latestVersion==null)
                GetLatestVersion();

            return _latestVersion;
        }

        public bool RunningVersionIsBeta
        {
            get {
                if (String.IsNullOrEmpty(_latestReleaseVersion))
                    GetLatestVersion();
                return _runningVersionIsBeta;
            }
        }

        private bool WriteUpdateFile(string updateInfo)
        {
            if (File.Exists(ApplicationUpdateInfoFile()))
            {
                try
                {
                    File.Delete(ApplicationUpdateInfoFile());
                }
                catch
                {
                    return false;
                }
            }
            try
            {
                File.WriteAllText($"{Application.ProductName}.update", updateInfo);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string ApplicationUpdateInfoFile()
        {
            return $"{Application.ProductName}.update";
        }

        public void ClearUpdateFiles()
        {
            // Remove the Updater.exe and .update files, if they exist

            if (File.Exists(ApplicationUpdateInfoFile()))
            {
                try
                {
                    File.Delete(ApplicationUpdateInfoFile());
                }
                catch { }
            }
            if (File.Exists(UpdaterExeName()))
            {
                try
                {
                    File.Delete(UpdaterExeName());
                }
                catch { }
            }
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
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
            if (String.IsNullOrEmpty(availableVersionInfo))
            {
                MessageBox.Show("Failed to retrieve available versions");
                return;
            }

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

            foreach (VersionInfo versionInfo in _availableVersions)
            {
                if (versionInfo.isBeta)
                {
                    if (useBeta && versionInfo.IsHigherThan(ThisVersion))
                        _latestVersion = versionInfo;
                }
                else if (versionInfo.IsHigherThan(ThisVersion))
                    _latestVersion = versionInfo;
            }
            if (_latestVersion == null)
                return false;
            return true;
        }

        public string UpdaterExeName()
        {
            return $"{Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 4)}Updater.exe";
        }

        public bool DownloadUpdate(bool IncludeBeta = false)
        {
            if (!UpdateAvailable(IncludeBeta))
                return false;

            // If we get here, there is an update available.  Prompt whether to download and install.
            UpdaterForm updaterForm = new UpdaterForm(_latestVersion);
            if (updaterForm.ShowDialog()==DialogResult.No)
                return false;

            // To install, we copy this executable to Updater.exe, and run that to process the update
            try
            {
                string updaterPath = UpdaterExeName();
                if (File.Exists(updaterPath))
                    File.Delete(updaterPath);
                File.Copy(Application.ExecutablePath, updaterPath);
                if (WriteUpdateFile(_latestVersion.ToString()))
                {
                    LaunchApplication(updaterPath);
                    return true;
                }
                else
                {
                    MessageBox.Show($"Failed to write update information file", "Update error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Update error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public void LaunchApplication(string ApplicationPath)
        {
            var psi = new ProcessStartInfo();

            psi.FileName = ApplicationPath;
            _ = Process.Start(psi);
        }

    }
}
