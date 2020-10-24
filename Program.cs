using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRVTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Application.ExecutablePath.EndsWith("Updater.exe"))
            {
                // We're about to install an update
                try
                {
                    Application.Run(new UpdaterForm(new Updater()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Failed to start updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                Application.Run(new FormTracker());
        }
    }
}
