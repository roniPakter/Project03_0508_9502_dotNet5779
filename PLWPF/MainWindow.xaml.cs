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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BL.IBL bl;
        public DateTime SystemTime { get; set; }

        public MainWindow()
        {
            
            bl = BL.Factory.GetBL();
            SystemTime = bl.getSystemTime();
            InitializeComponent();
            systemTimeSet.DisplayDateStart = SystemTime;
        }
        
        private void Button_login(object sender, RoutedEventArgs e)
        {
            try
            {
                string TypeOfUser = bl.getTypeOfUser(int.Parse(idInput.Text));
                switch (TypeOfUser)
                {
                    case "Trainee":
                        TraineeWindow traineeWindow = new TraineeWindow(bl.getTrainee(int.Parse(idInput.Text)));
                        traineeWindow.Show();
                        Close();
                        break;
                    case "Tester":
                        TesterWindow testerWindow = new TesterWindow(bl.getTester(int.Parse(idInput.Text)));
                        testerWindow.Show();
                        Close();
                        break;
                }
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
            
        }

       
        private void IdInputBoxClicked(object sender, MouseButtonEventArgs e)
        {
            idInput.Text = "";
            logIn.IsDefault = true;
        }

        private void Button_SignupTester(object sender, RoutedEventArgs e)
        {
            AddTesterWindow addTesterWindow = new AddTesterWindow();
            addTesterWindow.Show();
        }

        private void Button_SignupTrainee(object sender, RoutedEventArgs e)
        {
            AddTraineeWindow addTraineeWindow = new AddTraineeWindow();
            addTraineeWindow.Show();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminPasswordWindow adminPasswordWindow = new AdminPasswordWindow(this, (bool)testingModeCheckBox.IsChecked);
            
            adminPasswordWindow.Show();
        }

        private void SystemTimeSet_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bl.setSystemTime(systemTimeSet.SelectedDate.Value);
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
        }
    }
}
