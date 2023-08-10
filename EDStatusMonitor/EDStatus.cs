using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusMonitor
{
    public class EDStatus
    {
        public delegate void EDEventHandler(object sender, string eventJson);
        public event EDEventHandler EventOccurred; 
        
        private JournalReader _journalReader = null;
        private StatusReader _statusReader = null;

        public EDStatus()
        {
            _journalReader = new JournalReader();
            _journalReader.StartMonitoring();
            _journalReader.InterestingEventOccurred += _journalReader_InterestingEventOccurred;
            _statusReader = new StatusReader();
            _statusReader.StatusChanged += _statusReader_StatusChanged;
        }

        public void ReplayJournal()
        {
            _journalReader.Replay();
        }

        private void _statusReader_StatusChanged(object sender, string statusJson)
        {
            EventOccurred?.Invoke(this, statusJson);
        }

        private void _journalReader_InterestingEventOccurred(object sender, string eventJson)
        {
            EventOccurred?.Invoke(this, eventJson);
        }
    }
}
