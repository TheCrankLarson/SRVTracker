using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDTracking
{
    /// <summary>
    /// Monitor the status files (status.json and journal logs) and provide events to consumers
    /// </summary>
    /// 
    public class EventWatcher
    {
        private System.Timers.Timer _updateTimer = new System.Timers.Timer();
        private StatusReader _statusReader;
        private JournalReader _journalReader;

        /// <summary>
        /// Start watching for event updates
        /// </summary>
        public EventWatcher()
        {
            _journalReader = new JournalReader("");
        }
    }
}
