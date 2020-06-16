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
    /// Interaction logic for UpdateTesterWindow.xaml
    /// </summary>
    public partial class UpdateTesterWindow : Window
    {
        public BL.IBL bl { get; set; }
        public Tester UpdatedTester { get; set; }
        public ListSchedule ListSchedule { get; set; }
        public UpdateTesterWindow(Tester tester)
        {
            UpdatedTester = tester;
            DataContext = UpdatedTester;
            bl = BL.Factory.GetBL();
            InitializeComponent();
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            carTypeComboBox.ItemsSource = Enum.GetValues(typeof(VehicleType));
            addressGrid.DataContext = UpdatedTester.Address;
            ListSchedule = bl.getTesterSchedule(UpdatedTester);
            
            schedule.ItemsSource = ListSchedule.ScheduleAsList;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void UpdateTesterClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (id.Text != "" &&
                firstName.Text != "" &&
                lastName.Text != "" &&
                dateOfbirth.Text != "" &&
                genderComboBox.Text != "" &&
                phone.Text != "" &&
                street.Text != "" &&
                houseNumber.Text != "" &&
                city.Text != "" &&
                carTypeComboBox.Text != "" &&
                seniority.Text != "" &&
                maxDistance.Text != "" &&
                maxTests.Text != ""
                )
                {
                    UpdatedTester.Address = new Address(street.Text, int.Parse(houseNumber.Text), city.Text);
                    bl.setTesterSchedule(UpdatedTester, ListSchedule);
                    bl.updateTester(UpdatedTester.ID, UpdatedTester);
                    MessageBox.Show("Data updated on system", "", MessageBoxButton.OK, MessageBoxImage.Information);
                   Close();
                }
                else MessageBox.Show(
                    "Please fill all the fields before updating.",
                    "Missing Data",
                    MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);

            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }

        }

        private void CencelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
