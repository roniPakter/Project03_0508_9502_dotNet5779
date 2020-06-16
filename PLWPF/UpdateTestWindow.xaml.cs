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
using System.Windows.Shapes;
using BO;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateTestWindow.xaml
    /// </summary>
    public partial class UpdateTestWindow : Window
    {
        public Test UpdatedTest { get; set; }
        public BL.IBL bl { get; set; }
        
        public UpdateTestWindow(Test test)
        {
            UpdatedTest = test;
            bl = BL.Factory.GetBL();
            DataContext = UpdatedTest;
            InitializeComponent();
        }

        private void updateTestButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateTestAsAdmin(UpdatedTest.TestNumber, UpdatedTest);
                MessageBox.Show("Test Feedback was updated in the system", "Feedback Saved", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Close();
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }

        }
    }
}
