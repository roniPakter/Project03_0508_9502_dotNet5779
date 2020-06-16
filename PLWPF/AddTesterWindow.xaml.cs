using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Collections.ObjectModel;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTesterWindow.xaml
    /// </summary>
    public partial class AddTesterWindow : Window
    {
        private BL.IBL bl { get; set; }
        public ListSchedule ListSchedule { get; private set; }

        
        public AddTesterWindow()
        {
            InitializeComponent();
            bl = BL.Factory.GetBL();
            ListSchedule = new ListSchedule();
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            
            carTypeComboBox.ItemsSource = Enum.GetValues(typeof(VehicleType));
           
            schedule.ItemsSource = ListSchedule.ScheduleAsList;
        }
    

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Subscribe(object sender, RoutedEventArgs e)
        {
            try
            {

                if (id.Text == "" ||               
                    lastName.Text == "" ||               
                    firstName.Text == "" ||              
                    dateOfBrith.Text == "" ||               
                    genderComboBox.Text == "" ||               
                    phone.Text == "" ||               
                    street.Text == "" ||               
                    houseNumber.Text == "" ||               
                    city.Text == "" ||              
                    seniority.Text == "" ||               
                    maxTestsPerWeek.Text == "" ||
                    carTypeComboBox.Text == "" ||
                    maxDistanceFromAddress.Text == "")
                {
                    MessageBox.Show("Please fill all the fields before registerring", "Missing Input Data", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                

                Tester tester = new Tester(
                    int.Parse(id.Text),
                    lastName.Text,
                    firstName.Text,
                    dateOfBrith.SelectedDate.Value,
                    (Gender)genderComboBox.SelectedIndex,
                    phone.Text,
                    new Address(street.Text, int.Parse(houseNumber.Text), city.Text),
                    int.Parse(seniority.Text),
                    int.Parse(maxTestsPerWeek.Text),
                    (VehicleType)carTypeComboBox.SelectedIndex,
                    double.Parse(maxDistanceFromAddress.Text),
                    new List<TesterTest>());
                bl.setTesterSchedule(tester, ListSchedule);
                bl.addTester(tester);
                MessageBox.Show("Your details have successfully recorded in the system.\nUse the ID number to log in.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();

            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }
        }
    }
}
