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
using System.ComponentModel;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TestOrder.xaml
    /// </summary>
    public partial class TestOrder : Window
    {
        private BL.IBL bl { get; set; }
        public Trainee Trainee { get; set; }
        public BackgroundWorker AddTestWorker { get; set; }
        public SystemIsWorkingWindow systemIsWorkingWindow { get; set; }
        public TestOrder(Trainee trainee)
        {

            AddTestWorker = new BackgroundWorker();
            AddTestWorker.WorkerSupportsCancellation = true;
            systemIsWorkingWindow = new SystemIsWorkingWindow(AddTestWorker);
            bl = BL.Factory.GetBL();
            Trainee = trainee;
            DataContext = Trainee;
            InitializeComponent();
        }

        private void OrderTestClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Date.Text == "" ||
                Time.Text == "" ||
                Street.Text == "" ||
                HouseNumber.Text == ""
                || City.Text == "")
                {
                    MessageBox.Show("Please fill all the fields before requesting a test.");
                    return;
                }

                bl.addTest(
                    AddTestWorker,
                    Trainee,
                    Date.SelectedDate.Value,
                    int.Parse(Time.Text),
                    new Address(Street.Text, int.Parse(HouseNumber.Text), City.Text));
                systemIsWorkingWindow.Show();
                AddTestWorker.RunWorkerCompleted += AddTestWorker_RunWorkerCompleted;
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }

        }

        private void AddTestWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            systemIsWorkingWindow.Close();
            if (e.Cancelled)
            {
                MessageBox.Show(
                       "The test request has been cacelled.",
                       "Cancelled",
                       MessageBoxButton.OK,
                       MessageBoxImage.Information);
                Close();
                return;
            }
            if (e.Result is int)
            {
                MessageBox.Show(
                        "A test has been fixed.\nTest number is: " + ((int)e.Result).ToString(),
                        "Success!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                Close();
                return;
            }
            if (e.Result is Exception)
            {
                Exception exception = e.Result as Exception;
                HandleExceptions.Handle(exception, this);
            }

        }

        private void CencelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
