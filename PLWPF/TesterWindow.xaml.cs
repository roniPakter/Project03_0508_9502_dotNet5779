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
    /// Interaction logic for TesterWindow.xaml
    /// </summary>
    public partial class TesterWindow : Window
    {
        public BL.IBL bl { get; set; }
        public Tester Tester { get; set; }
        public ListSchedule ListSchedule { get; set; }

        public TesterWindow(Tester tester)
        {
            InitializeComponent();
            bl = BL.Factory.GetBL();
            Tester = tester;
            DataContext = Tester;
            ListSchedule = bl.getTesterSchedule(Tester);           
            schedule.ItemsSource = ListSchedule.ScheduleAsList;
        }

        private void UpdateDetailsButtonClick(object sender, RoutedEventArgs e)
        {
            UpdateTesterWindow Update = new UpdateTesterWindow(new Tester(Tester));
            Update.Closed += DetailsUpdated;
            Update.Show();
        }

        private void SignOutClicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void DetailsUpdated(object sender, EventArgs e)
        {
            try
            {
                Tester = bl.getTester(Tester.ID);
                ListSchedule = bl.getTesterSchedule(Tester);
                DataContext = Tester;
                schedule.ItemsSource = ListSchedule.ScheduleAsList;
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
            
        }

        private void DeleteTester(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to remove all details from the system?", "Delete Tester",
                               MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bl.removeTester(Tester.ID);
                    Close();
                }
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }

        }

        private void GiveFeedbackButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                TestFeedbackWindow testFeedBackWindow = new TestFeedbackWindow(
                    bl.getTestswaitingForfeedback(Tester.ID),
                    Tester.FirstName + " " + Tester.LastName,
                    Tester.ID);
                testFeedBackWindow.Show();
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
        }

        private void DeleteTestClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want abort this test?", "Delete Test",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Button button = sender as Button;
                    int testNumber = (int)button.Tag;
                    bl.removeTest(testNumber);
                    MessageBox.Show("The test is aborted.\n", "Test Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    Tester = bl.getTester(Tester.ID);
                    DataContext = Tester;
                }
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
        }

        private void updateSchedule_Click(object sender, RoutedEventArgs e)
        {
            schedulePageTitle.Text = "Click on the checkboxes to change the schedule";
            updateSchedule.Visibility = Visibility.Hidden;
            confirmScheduleChanges.Visibility = Visibility.Visible;
            backToOriginalSchedule.Visibility = Visibility.Visible;
            schedule.IsReadOnly = false;
            
        }
        

        private void confirmScheduleChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                schedulePageTitle.Text = "Tester's Weekly Work Schedule";
                bl.setTesterSchedule(Tester, ListSchedule);
                bl.updateTester(Tester.ID, Tester);
                DetailsUpdated(null, null);
                confirmScheduleChanges.Visibility = Visibility.Hidden;
                backToOriginalSchedule.Visibility = Visibility.Hidden;
                updateSchedule.Visibility = Visibility.Visible;
                schedule.IsReadOnly = true;
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
        }

        private void backToOriginalSchedule_Click(object sender, RoutedEventArgs e)
        {
            schedulePageTitle.Text = "Tester's Weekly Work Schedule";
            DetailsUpdated(null, null);
            confirmScheduleChanges.Visibility = Visibility.Hidden;
            backToOriginalSchedule.Visibility = Visibility.Hidden;
            updateSchedule.Visibility = Visibility.Visible;
            schedule.IsReadOnly = true;
        }

       
    }
}
