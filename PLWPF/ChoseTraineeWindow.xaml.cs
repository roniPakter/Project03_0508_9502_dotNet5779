using BO;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ChoseTraineeWindow.xaml
    /// </summary>
    public partial class ChoseTraineeWindow : Window
    {
        public ChoseTraineeWindow(List<Trainee> trainees)
        {
            InitializeComponent();
            traineesListView.ItemsSource = trainees;
            
        }

        private void TraineesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object dataContext = ((FrameworkElement)e.OriginalSource).DataContext;
            if (dataContext is Trainee)
            {
                TestOrder testOrder = new TestOrder((Trainee)traineesListView.SelectedItem);
                testOrder.Show();
                Close();
            }
        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {

            if (traineesListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a trainee to update from the list");
                return;
            }
            Trainee trainee = traineesListView.SelectedItem as Trainee;
            TestOrder testOrder = new TestOrder(trainee);
            testOrder.Show();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
