using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ListSchedule
    {
        //Declare a list that contains Schedule objects
        public ObservableCollection<coulmonInputs> ScheduleAsList { get; set; }

        //For each coulmon need a property
        public class coulmonInputs
        {
            //The current day
            public string Day { get; set; }
            //The hours are from 9:00 to 14:00
            public bool Nine { get; set; }
            public bool Ten { get; set; }
            public bool Eleven { get; set; }
            public bool Twelve { get; set; }
            public bool Thirteen { get; set; }
            public bool Fourteen { get; set; }

            //A constructor that updates the names of the days and all hours leaves by default of false
            public coulmonInputs(string day)
            {
                Day = day;
            }
            //a constructor that updates the names of the days and all the hours
            public coulmonInputs(string day, bool nine, bool ten, bool eleven, bool tw, bool th, bool fo)
            {
                Day = day;
                Nine = nine;
                Ten = ten;
                Eleven = eleven;
                Twelve = tw;
                Thirteen = th;
                Fourteen = fo;
            }
        }
        //a constructor that adds all days and times by default
        public ListSchedule()
        {
            ScheduleAsList = new ObservableCollection<coulmonInputs>();
            ScheduleAsList.Add(new coulmonInputs("Sunday"));
            ScheduleAsList.Add(new coulmonInputs("Monday"));
            ScheduleAsList.Add(new coulmonInputs("Tuesday"));
            ScheduleAsList.Add(new coulmonInputs("Wednesday"));
            ScheduleAsList.Add(new coulmonInputs("Thursday"));
        }
    }
}
