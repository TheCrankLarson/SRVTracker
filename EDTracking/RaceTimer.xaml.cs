using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EDTracking
{
    /// <summary>
    /// Interaction logic for RaceTimer.xaml
    /// </summary>
    public partial class RaceTimer : UserControl
    {
        public event EventHandler MouseIsOver;
        public event EventHandler MouseIsNotOver;

        public RaceTimer()
        {
            InitializeComponent();
        }

        public void SetTimer(TimeSpan time)
        {           
            if (this.Dispatcher.CheckAccess())
                textBlock.Text = time.ToString(@"mm\:ss\:ff");
            else
                this.Dispatcher.Invoke(() =>
                {
                    textBlock.Text = time.ToString(@"mm\:ss\:ff");
                });
        }

        private void textBlock_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                MouseIsOver?.Invoke(this, null);
            else
                MouseIsNotOver?.Invoke(this, null);
        }

        private void Grid_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                MouseIsOver?.Invoke(this, null);
            else
                MouseIsNotOver?.Invoke(this, null);
        }
    }
}
