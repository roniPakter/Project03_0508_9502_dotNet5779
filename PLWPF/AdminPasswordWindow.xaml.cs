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
    /// Interaction logic for AdminPasswordWindow.xaml
    /// </summary>
    public partial class AdminPasswordWindow : Window
    {
        private MainWindow FatherWindow { get; set; }
        
        public AdminPasswordWindow(MainWindow fatherWindow, bool isTestingMode)
        {

            FatherWindow = fatherWindow;
            InitializeComponent();
            if (isTestingMode)
            {
                password.Password = "AvnerAndRoni";
            }
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (password.Password == "AvnerAndRoni" )
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                FatherWindow.Close();
                Close();
            }
        }

        private void CencelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
