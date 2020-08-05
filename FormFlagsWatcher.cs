using EDTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRVTracker
{
    public partial class FormFlagsWatcher : Form
    {
        string[] _flagNames;
        System.Array _flagValues;
        long _currentFlags = 0;

        public FormFlagsWatcher()
        {
            InitializeComponent();
            Type flagsType = typeof(StatusFlags);
            _flagNames = flagsType.GetEnumNames();
            _flagValues = flagsType.GetEnumValues();
            InitFlagsList();
        }

        private void InitFlagsList()
        {
            listViewCurrentFlags.Items.Clear();
            listViewCurrentFlags.BeginUpdate();
            for (int i = 0; i < _flagNames.Length; i++)
                listViewCurrentFlags.Items.Add(new ListViewItem(_flagNames[i]));
            listViewCurrentFlags.EndUpdate();
        }

        private bool isFlagSet(int flagIndex)
        {
            return ((_currentFlags & (long)_flagValues.GetValue(flagIndex)) == (long)_flagValues.GetValue(flagIndex));
        }

        private bool isFlagSet(int flagIndex, long flags)
        {
            return ((flags & (long)_flagValues.GetValue(flagIndex)) == (long)_flagValues.GetValue(flagIndex));
        }

        public void UpdateFlags(long flags)
        {
            listBoxStatusHistory.Items.Insert(0,_currentFlags);
            _currentFlags = flags;

            if (listBoxStatusHistory.SelectedIndex > -1)
                return; // We're not looking at current flags

            Action action = new Action(() =>
            {
                for (int i = 0; i < _flagNames.Length; i++)
                    listViewCurrentFlags.Items[i].Checked = isFlagSet(i);

            });

            if (listViewCurrentFlags.InvokeRequired)
                listViewCurrentFlags.Invoke(action);
            else
                action();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBoxStatusHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxStatusHistory.SelectedIndex < 0)
                return;

            long flags = (long)listBoxStatusHistory.SelectedItem;

            Action action = new Action(() =>
            {
                for (int i = 0; i < _flagNames.Length; i++)
                    listViewCurrentFlags.Items[i].Checked = isFlagSet(i, flags);

            });

            if (listViewCurrentFlags.InvokeRequired)
                listViewCurrentFlags.Invoke(action);
            else
                action();
        }

        private void buttonShowCurrent_Click(object sender, EventArgs e)
        {
            listBoxStatusHistory.SelectedIndex = -1;
        }
    }
}
