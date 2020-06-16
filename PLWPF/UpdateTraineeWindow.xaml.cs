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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class UpdateTraineeWindow : Window
    {
        BL.IBL bl;
        public Trainee UpdatedTrainee { get; set; }
        public UpdateTraineeWindow(Trainee trainee)
        {
            UpdatedTrainee = trainee;
            DataContext = UpdatedTrainee;
            bl = BL.Factory.GetBL();
            InitializeComponent();
            addressGrid.DataContext = UpdatedTrainee.Address;
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            carTypeComboBox.ItemsSource = Enum.GetValues(typeof(VehicleType));            
            gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(GearBox));
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void UpdateTraineeClicked(object sender, RoutedEventArgs e)
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
                gearBoxComboBox.Text != "" &&
                schoolName.Text != "" &&
                teacherName.Text != "" &&
                numOfLessons.Text != ""
                )
                {
                    UpdatedTrainee.Address = new Address(street.Text, int.Parse(houseNumber.Text), city.Text);
                    bl.updateTrainee(UpdatedTrainee.ID, UpdatedTrainee);
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
