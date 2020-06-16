using BO;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTraineeWindow.xaml
    /// </summary>
    public partial class AddTraineeWindow : Window
    {
        BL.IBL bl;
        public AddTraineeWindow()
        {
            InitializeComponent();
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            carTypeComboBox.ItemsSource = Enum.GetValues(typeof(VehicleType));
            gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(GearBox));
            bl = BL.Factory.GetBL();
        }

        private void AddTraineeButtonClicked(object sender, RoutedEventArgs e)
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
                    Trainee inputTrainee = new Trainee(
                        Int32.Parse(id.Text),
                        lastName.Text,
                        firstName.Text,
                        (DateTime)dateOfbirth.SelectedDate,
                        (Gender)genderComboBox.SelectedItem,
                        phone.Text,
                        new Address(street.Text, int.Parse(houseNumber.Text), city.Text),
                        (VehicleType)carTypeComboBox.SelectedItem,
                        (GearBox)gearBoxComboBox.SelectedItem,
                        schoolName.Text,
                        teacherName.Text,
                        int.Parse(numOfLessons.Text),
                        new List<TraineeTest>());
                    bl.addTrainee(inputTrainee);
                    MessageBox.Show("Your details have successfully recorded in the system.\nUse your ID number to log in any time you wish.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Please fill all the fields before registerring", "Missing Input Data", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
            }
            catch (Exception exception)
            {
                HandleExceptions.Handle(exception, this);
            }




        }      

        private  void TextBoxClicked(object sender, MouseEventArgs e)
        {
            TextBox senderBox = (TextBox)sender;
            senderBox.Text = "";
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }

    
}
