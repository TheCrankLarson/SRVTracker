using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public UpdaterForm()
        {
            InitializeComponent();
            textBoxThisVersion.Text = Application.ProductVersion;
        }

        public UpdaterForm(VersionInfo updateInformation): this()
        {
            _updateInformation = updateInformation;
            textBoxAvailableVersion.Text = _updateInformation.version;           
            groupBoxUpdating.Visible = false;
        }

        public UpdaterForm(Updater updater): this()
        {
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
                Close();
            });
            Task.Run(action);
        }

        private void DownloadAndExtractZip()
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
                    }
                }
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
                //textBoxUpdateProgress.Refresh();
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
