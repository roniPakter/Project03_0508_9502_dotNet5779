using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BL;
using BO;

namespace PLWPF
{
    public class HandleExceptions
    {
        public static void Handle(Exception exception, Window windowSender = null)
        {
            IBL bl = Factory.GetBL();
            if (exception is AskedDateIsNotAvailableException)
            {
                var ex = exception as AskedDateIsNotAvailableException;
                MessageBoxResult result = MessageBox.Show(
            "No available tester in the asked date.\nDo you want to appoint a test in the alternative date:\n"
            + ex.AlternativeDate.DayOfWeek.ToString() + " "
            + (ex.AlternativeDate.ToShortDateString()
            + ", on " + ex.AlternativeTime.ToString()) + " o'clock?",
            "Alternative Date Suggestion",
            MessageBoxButton.YesNo,
            MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    bl.addTestWithFoundTester(ex.Trainee, ex.AlternativeDate, ex.AlternativeTime, ex.ExitAddress, ex.FoundTester);
                    MessageBox.Show(
                "A test has been fixed.\n",
                "Success!",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
                    windowSender.Close();
                }
            }
            else
                MessageBox.Show(exception.Message, "An Error Has Occured", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
