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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private BL.IBL bl { set; get; }
        public AdminWindow()
        {
            try
            {
                bl = BL.Factory.GetBL();
                InitializeComponent();
                testersListView.ItemsSource = bl.getTestersByVehicleType();
                traineesListView.ItemsSource = bl.getTraineesBySchool();
                testsListView.ItemsSource = bl.getTests();
                testerCityFilterCombo.ItemsSource = bl.getCitiesOfTesters();
                testerGenderFilterCombo.ItemsSource = Enum.GetValues(typeof(Gender));
                testerVehicleFilterCombo.ItemsSource = Enum.GetValues(typeof(VehicleType));
                traineeCityFilterCombo.ItemsSource = bl.getCitiesOfTrainees();
                traineeGenderFilterCombo.ItemsSource = Enum.GetValues(typeof(Gender));
                traineeVehicleFilterCombo.ItemsSource = Enum.GetValues(typeof(VehicleType));
            }
            catch (Exception ex)
            {
                HandleExceptions.Handle(ex, this);
            }
        }

        private void SignOutClicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void AddTester_Click(object sender, RoutedEventArgs e)
        {
            if (testersListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a tester to update from the list");
                return;
            }
            Tester tester = testersListView.SelectedItem as Tester;
            AddTesterWindow AddTesterWindow = new AddTesterWindow();
            IsEnabled = false;
            AddTesterWindow.Closed += ChildWindowClosed;
            AddTesterWindow.Show();
        }

        private void ChildWindowClosed(object sender, EventArgs e)
        {
            IsEnabled = true;
        }

        private void UpdateTester_Click(object sender, RoutedEventArgs e)
        {
            if (testersListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a tester to update from the list");
                return;
            }
            Tester tester = testersListView.SelectedItem as Tester;
            UpdateTesterWindow updateTesterWindow = new UpdateTesterWindow(tester);
            IsEnabled = false;
            updateTesterWindow.Show();
            updateTesterWindow.Closed += ChildWindowClosed;
           
        }

        private void DeleteTester_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (testersListView.SelectedItem == null)
                {
                    MessageBox.Show("Please select a tester to update from the list");
                    return;
                }
                Tester tester = testersListView.SelectedItem as Tester;

                MessageBoxResult result = MessageBox.Show("Are you sure you want abort this tester?", "Delete Test",
                                   MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bl.removeTester(tester.ID);
                    MessageBox.Show("The tester is deleted.\n", "Tester Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                HandleExceptions.Handle(ex, this);
            }
        }
        private void AddTrainee_Click(object sender, RoutedEventArgs e)
        {
            if (testersListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a trainee to update from the list");
                return;
            }
            Tester tester = testersListView.SelectedItem as Tester;

            AddTraineeWindow addTraineeWindow = new AddTraineeWindow();
            IsEnabled = false;
            addTraineeWindow.Show();
            addTraineeWindow.Closed += ChildWindowClosed;

        }

        private void UpdateTrainee_Click(object sender, RoutedEventArgs e)
        {       
            if (traineesListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a trainee to update from the list");
                return;
            }
            Trainee trainee = traineesListView.SelectedItem as Trainee;
            UpdateTraineeWindow updateTraineeWindow = new UpdateTraineeWindow(trainee);
            IsEnabled = false;
            updateTraineeWindow.Show();
            updateTraineeWindow.Closed += ChildWindowClosed;
        }

        private void DeleteTrainee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (traineesListView.SelectedItem == null)
                {
                    MessageBox.Show("Please select a trainee to delete from the list");
                    return;
                }
                Trainee trainee = traineesListView.SelectedItem as Trainee;

                MessageBoxResult result = MessageBox.Show("Are you sure you want dealete this trainee?", "Delete Ttainee",
                       MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bl.removeTrainee(trainee.ID);
                    MessageBox.Show("The trainee is deleted.\n", "Trainee Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                HandleExceptions.Handle(ex, this);
            }
            
        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Trainee> allowedForTest = bl.getAllTraineesAllowedForTest();
                ChoseTraineeWindow choseTraineeWindow = new ChoseTraineeWindow(allowedForTest);
                choseTraineeWindow.Show();
                IsEnabled = false;
                choseTraineeWindow.Closed += ChildWindowClosed;
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
        }
        private void UpdateTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (testsListView.SelectedItem == null)
                {
                    MessageBox.Show("Please select a test to update from the list");
                    return;
                }
                Test test = testsListView.SelectedItem as Test;
                UpdateTestWindow updateTestWindow = new UpdateTestWindow(test);
                IsEnabled = false;
                updateTestWindow.Show();
                updateTestWindow.Closed += ChildWindowClosed;
            }
            catch (Exception ex)
            {
                HandleExceptions.Handle(ex, this);
            }
        }

        private void DeleteTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (testsListView.SelectedItem == null)
                {
                    MessageBox.Show("Please select a test to delete from the list");
                    return;
                }
                Test test = testsListView.SelectedItem as Test;

                MessageBoxResult result = MessageBox.Show("Are you sure you want dealete this test?", "Delete Test",
                       MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bl.removeTest(test.TestNumber);
                    MessageBox.Show("The test is deleted.\n", "Test Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                HandleExceptions.Handle(ex, this);
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object dataContext = ((FrameworkElement)e.OriginalSource).DataContext;
            if (dataContext is Tester)
            {
                TesterWindow testerWindow = new TesterWindow((Tester)testersListView.SelectedItem);
                testerWindow.Show();
                testerWindow.Closed += ChildWindowClosed;
                IsEnabled = false;
                return;
            }
            if (dataContext is Trainee)
            {
                TraineeWindow traineeWindow = new TraineeWindow((Trainee)traineesListView.SelectedItem);
                traineeWindow.Show();
                traineeWindow.Closed += ChildWindowClosed;
                IsEnabled = false;
                return;
            }
        }

        private void TraineeFiltersChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Trainee> traineesList = bl.getTrainees();
                if (traineeCityFilterCheck.IsChecked == true && traineeCityFilterCombo.SelectedItem != null)
                {
                    traineesList = (from Trainee trainee in traineesList
                                    where trainee.Address.City == (string)traineeCityFilterCombo.SelectedItem
                                   select trainee).ToList();
                }
                if (traineeGenderFilterCheck.IsChecked == true && traineeGenderFilterCombo.SelectedItem != null)
                {
                    traineesList = (from Trainee trainee in traineesList
                                   where trainee.Gender == (Gender)traineeGenderFilterCombo.SelectedItem
                                   select trainee).ToList();
                }
                if (traineeVehicleFilterCheck.IsChecked == true && traineeVehicleFilterCombo.SelectedItem != null)
                {
                    traineesList = (from Trainee trainee in traineesList
                                    where trainee.CarType == (VehicleType)traineeVehicleFilterCombo.SelectedItem
                                   select trainee).ToList();
                }
                traineesListView.ItemsSource = traineesList;
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception);
            }
        }

        private void TraineeFiltersChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<Trainee> traineesList = bl.getTrainees();
                if (traineeCityFilterCheck.IsChecked == true && traineeCityFilterCombo.SelectedItem != null)
                {
                    traineesList = (from Trainee trainee in traineesList
                                    where trainee.Address.City == (string)traineeCityFilterCombo.SelectedItem
                                    select trainee).ToList();
                }
                if (traineeGenderFilterCheck.IsChecked == true && traineeGenderFilterCombo.SelectedItem != null)
                {
                    traineesList = (from Trainee trainee in traineesList
                                    where trainee.Gender == (Gender)traineeGenderFilterCombo.SelectedItem
                                    select trainee).ToList();
                }
                if (traineeVehicleFilterCheck.IsChecked == true && traineeVehicleFilterCombo.SelectedItem != null)
                {
                    traineesList = (from Trainee trainee in traineesList
                                    where trainee.CarType == (VehicleType)traineeVehicleFilterCombo.SelectedItem
                                    select trainee).ToList();
                }
                traineesListView.ItemsSource = traineesList;
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception);
            }
        }

        private void TesterFiltersChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Tester> testersList = bl.getTesters();
                if (testerCityFilterCheck.IsChecked == true && testerCityFilterCombo.SelectedItem != null)
                {
                    testersList = (from Tester tester in testersList
                                   where tester.Address.City == (string)testerCityFilterCombo.SelectedItem
                                   select tester).ToList();
                }
                if (testerGenderFilterCheck.IsChecked == true && testerGenderFilterCombo.SelectedItem != null)
                {
                    testersList = (from Tester tester in testersList
                                   where tester.Gender == (Gender)testerGenderFilterCombo.SelectedItem
                                   select tester).ToList();
                }
                if (testerVehicleFilterCheck.IsChecked == true && testerVehicleFilterCombo.SelectedItem != null)
                {
                    testersList = (from Tester tester in testersList
                                   where tester.CarType == (VehicleType)testerVehicleFilterCombo.SelectedItem
                                   select tester).ToList();
                }
                testersListView.ItemsSource = testersList;
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception);
            }
        }

        private void TesterFiltersChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<Tester> testersList = bl.getTesters();
                if (testerCityFilterCheck.IsChecked == true && testerCityFilterCombo.SelectedItem != null)
                {
                    testersList = (from Tester tester in testersList
                                   where tester.Address.City == (string)testerCityFilterCombo.SelectedItem
                                   select tester).ToList();
                }
                if (testerGenderFilterCheck.IsChecked == true && testerGenderFilterCombo.SelectedItem != null)
                {
                    testersList = (from Tester tester in testersList
                                   where tester.Gender == (Gender)testerGenderFilterCombo.SelectedItem
                                   select tester).ToList();
                }
                if (testerVehicleFilterCheck.IsChecked == true && testerVehicleFilterCombo.SelectedItem != null)
                {
                    testersList = (from Tester tester in testersList
                                   where tester.CarType == (VehicleType)testerVehicleFilterCombo.SelectedItem
                                   select tester).ToList();
                }
                testersListView.ItemsSource = testersList;
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception);
            }
        }

        private void configuration(object sender, RoutedEventArgs e)
        {
            ConfigWindow configWindow = new ConfigWindow();
            configWindow.Show();
        }
        private void getCloseTesters(object sender, RoutedEventArgs e)
        {
            //CloseTestersWindow closeTestersWindow = new CloseTestersWindow();
            //closeTestersWindow.Show();
        }
        private void getAvailableTesters(object sender, RoutedEventArgs e)
        {
            //TesterListWindow testerListWindow = new TesterListWindow();
            //testerListWindow.Show();
        }
        private void getCertifiedTrainees(object sender, RoutedEventArgs e)
        {
            //TraineesListWindow traineesListWindow = new TraineesListWindow();
            //traineesListWindow.Show();
        }
        private void getTestsByDate(object sender, RoutedEventArgs e)
        {
            //TestsListWindow testsListWindow = new TestsListWindow();
            //testsListWindow.Show();
        }
        private void getFailedTests(object sender, RoutedEventArgs e)
        {
            //TestsListWindow testsListWindow = new TestsListWindow();
            //testsListWindow.Show();
        }
    }
}
