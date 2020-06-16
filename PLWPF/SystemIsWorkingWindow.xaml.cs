using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for SystemIsWorkingWindow.xaml
    /// </summary>
    public partial class SystemIsWorkingWindow : Window
    {
        private BackgroundWorker Worker { get; set; }
        public SystemIsWorkingWindow(BackgroundWorker worker)
        {
            Worker = worker;
            InitializeComponent();

        }

        private void CencelButton_Click(object sender, RoutedEventArgs e)
        {
            Worker.CancelAsync();
        }
    }
}
