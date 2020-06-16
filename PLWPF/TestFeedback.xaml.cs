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
    /// Interaction logic for TestFeedback.xaml
    /// </summary>
    public partial class TestFeedbackWindow : Window
    {
        public BL.IBL bl { get; set; }
        public int TesterID { get; set; }
        public List<TesterTest> Tests { get; set; }
        public string TesterName { get; private set; }
        public TesterTest UpdatedTest { get; set; }
      
        public TestFeedbackWindow(List<TesterTest> tests, string testerFullName, int testerID)
        {
            bl = BL.Factory.GetBL();
            Tests = tests;
            TesterName = testerFullName;
            TesterID = testerID;
            Title ="Tests Waiting For Feedback By: " + TesterName;
            DataContext = Tests;
            InitializeComponent();
        }


        private void doneButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void updateTestButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                UpdatedTest = button.Tag as TesterTest;
                bl.updateTest(UpdatedTest.TestNumber, UpdatedTest, TesterID);
                button.IsEnabled = false;
                MessageBox.Show("Test Feedback was updated in the system", "Feedback Saved", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }

        }

    }
}
