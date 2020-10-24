using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace SRVTracker
{
    public partial class UpdaterForm : Form
    {
        private VersionInfo _updateInformation = null;
        private Updater _updater = null;
        private bool _errorPreventsClose = false;

        public UpdaterForm()
        {
            InitializeComponent();
            textBoxThisVersion.Text = Application.ProductVersion;
        }

        public UpdaterForm(VersionInfo updateInformation): this()
        {
            // This is the entry point to prompt whether to update ot not - so we hide the updating group box so that the update options can be seen
            _updateInformation = updateInformation;
            textBoxAvailableVersion.Text = _updateInformation.version;           
            groupBoxUpdating.Visible = false;
        }

        public UpdaterForm(Updater updater): this()
        {
            // This is the entry point if we are updating.  The .update file should contain the update information
            try
            {
                string updateInfo = File.ReadAllText($"{Application.ProductName}.update");
                _updateInformation = VersionInfo.FromString(updateInfo);
            }
            catch { }
            if (_updateInformation == null)
            {
                MessageBox.Show("Failed to get latest version", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                groupBoxUpdating.Visible = false;
                buttonYes.Enabled = false;
                return;
            }

            _updater = updater;
            this.Width = 800;
            groupBoxUpdating.Visible = true;
            Action action = new Action(() =>
            {
                InstallUpdate();
                if (!_errorPreventsClose) // Close is prevented if a potentially fatal error occurred during update
                    Close();
            });
            Task.Run(action);
        }

        private void DownloadAndExtractZip()
        {
            // Download the latest zip from the update Url, and extract the contents

            try
            {
                WebRequest wrq = WebRequest.Create(_updateInformation.downloadUrl);
                WebResponse wrs = wrq.GetResponse();
                AddLog("Update retrieved");
                using (var stm = wrs.GetResponseStream())
                {
                    var zip = new ZipArchive(stm);
                    foreach (var entry in zip.Entries)
                    {

                        try
                        {
                            AddLog($"Deleting {entry.FullName}");
                            File.Delete(entry.FullName);
                        }
                        catch (Exception ex)
                        {
                            AddLog($"Error: {ex.Message}");
                        }
                        var d = Path.GetDirectoryName(entry.FullName);
                        if (!string.IsNullOrEmpty(d))
                        {
                            try
                            {
                                AddLog($"Creating directory {d}");
                                Directory.CreateDirectory(d);
                            }
                            catch (Exception ex)
                            {
                                AddLog($"Error: {ex.Message}");
                            }
                        }
                        AddLog($"Writing {entry.FullName}");
                        try
                        {
                            using (Stream zipStream = entry.Open())
                            using (FileStream fileStream = File.OpenWrite(entry.FullName))
                                zipStream.CopyTo(fileStream);
                        }
                        catch (Exception ex)
                        {
                            AddLog($"Error: {ex.Message}");
                            _errorPreventsClose = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog($"Error: {ex.Message}");
                _errorPreventsClose = true;
            }
        }

        private void AddLog(string info)
        {
            Action action = new Action(() =>
            {
                if (String.IsNullOrEmpty(textBoxUpdateProgress.Text))
                    textBoxUpdateProgress.Text = info;
                else
                    textBoxUpdateProgress.Text = $"{textBoxUpdateProgress.Text}{Environment.NewLine}{info}";
                textBoxUpdateProgress.SelectionStart = textBoxUpdateProgress.TextLength-1;
                textBoxUpdateProgress.ScrollToCaret();
            });
            if (textBoxUpdateProgress.InvokeRequired)
                textBoxUpdateProgress.Invoke(action);
            else
                action();
        }

        private void InstallUpdate()
        {
            AddLog($"Updating to version {_updateInformation.version}");
            AddLog($"Starting update from {_updateInformation.downloadUrl}");
            DownloadAndExtractZip();
            AddLog($"Update complete.  Restarting application.");
            string appPath = $"{Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 11)}.exe";
            _updater.LaunchApplication(appPath);
        }

        private void buttonNo_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.Modal)
                Close();
        }
    }
}
