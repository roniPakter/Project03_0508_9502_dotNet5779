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
using static BL.ExtensionMethods;
using System.Text.RegularExpressions;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Trainee.xaml
    /// </summary>
    public partial class TraineeWindow : Window
    {
        BL.IBL bl;
        public Trainee Trainee { get; set; }
        public TraineeWindow(Trainee trainee)
        {
            bl = BL.Factory.GetBL();
            Trainee = trainee;        
            DataContext = Trainee;
            InitializeComponent();
        }

        private void UpdateDetailsButtonClick(object sender, RoutedEventArgs e)
        {
            UpdateTraineeWindow  Update = new UpdateTraineeWindow(new Trainee(Trainee));
            Update.Closed += DetailsUpdated;
            Update.Show();
        }

        private void TestOrderButtonClick(object sender, RoutedEventArgs e)
        {
            TestOrder TestOrder = new TestOrder(Trainee);
            TestOrder.Closed += DetailsUpdated;
            TestOrder.Show();
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
                Trainee = bl.getTrainee(Trainee.ID);
                DataContext = Trainee;
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
        }

        private void DeleteTrainee(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to delete?", "Delete trainee",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bl.removeTrainee(Trainee.ID);
                    Close();
                }
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
                    MessageBox.Show("The test is aborted.\nYou can appoint a new test by the \"Order Test\" button.", "Test Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    Trainee = bl.getTrainee(Trainee.ID);
                    DataContext = Trainee;
                }
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
            
        }

        private void IncreaseLessonsCounterClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show(
                "You added one more driving lesson to the counter.\nThe updated sum of lessons is: "
                + (Trainee.NumberOfLessons + 1).ToString(),
                "Adding a Lesson",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    Trainee.NumberOfLessons++;
                    bl.updateTrainee(Trainee.ID, Trainee);
                    Trainee = bl.getTrainee(Trainee.ID);
                    DataContext = Trainee;
                }
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
            
        }
    }
}
