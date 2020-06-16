using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DL
{
    internal class DataSource
    {
        public static List<Tester> TesterList = new List<Tester>();
        public static List<Trainee> TraineeList = new List<Trainee>();
        public static List<Test> TestList = new List<Test>();

        public class ConfigurationParameter
        {
            public bool Readable { get; set; }
            public bool Writeable { get; set; }
            public object Value { get; set; }

            public ConfigurationParameter(bool readable, bool writeable, object val)
            {
                Readable = readable;
                Writeable = writeable;
                Value = val;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        public Dictionary<string, ConfigurationParameter> Configuration { get; set; }
        public Dictionary<int, bool[,]> Schedules { get; set; }

        public DataSource()
        {
            Configuration = new Dictionary<string, ConfigurationParameter>();
            Schedules = new Dictionary<int, bool[,]>();

            Configuration.Add("MinimumTesterAge", new ConfigurationParameter(true, true, 24));
            Configuration.Add("MinimumTraineeAge", new ConfigurationParameter(true, true, 17));
            Configuration.Add("DaysIntervalBetweenTests", new ConfigurationParameter(true, true, 14));
            Configuration.Add("MaximumTestersAge", new ConfigurationParameter(true, true, 65));
            Configuration.Add("MinimumNumberOfLessons", new ConfigurationParameter(true, true, 28));
            Configuration.Add("BeginningSerialTestNumber", new ConfigurationParameter(true, true, 10000000));
            Configuration.Add("MinimumPercentsOfPositiveGrades", new ConfigurationParameter(true, true, 50));
            Configuration.Add("TimeTestersBeginToWork", new ConfigurationParameter(true, true, 9));
            Configuration.Add("DaysBeforeTestATestCanBeDeleted", new ConfigurationParameter(true, true, 2));

            Schedules.Add(12345678, new bool[5, 6]);
            Schedules.Add(22222222, new bool[5, 6]);
            Schedules.Add(87654321, new bool[5, 6]);
            Schedules.Add(77777777, new bool[5, 6]);
            foreach (var c in Schedules)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 6; j++)
                        c.Value[i, j] = true;
                }
            }

            #region Instance Samples For Testing
            TesterList.Add(new Tester(
                12345678, 
                "Netanyahu", 
                "Bibi", 
                DateTime.Parse("2/5/1960"), 
                Gender.Male, 
                "050777788", 
                new Address("Balfur", 45, "Jerusalem"), 
                24, 
                18, 
                VehicleType.HeavyTrack, 60.5));
            TesterList.Add(new Tester(
                22222222,
                "Packter",
                "Aharon",
                DateTime.Parse("22-4-1990"),
                Gender.Male,
                "0527188935",
                new Address("Hibner", 15, "Petah-tikva"),
                15,
                30,
                VehicleType.TwoWheeled,
                220));
            TesterList.Add(new Tester(
                87654321, 
                "Choen", 
                "David", 
                DateTime.Parse("1/3/2000"),
                Gender.Male, 
                "052455567", 
                new Address("Ben-noon", 4, "Bnei-Brack"),
                5,
                10, 
                VehicleType.Private, 
                50.8));
            TesterList.Add(new Tester(
                77777777,
                "Erlich",
                "Uri",
                DateTime.Parse("3-5-1984"),
                Gender.Male,
                "054166778",
                new Address("Choni-Hameagel", 55, "Elad"),
                7,
                34,
                VehicleType.LightTrack,
                63.4));

            TraineeList.Add(new Trainee(
                32323232,
                "Purkaney",
                "Yotsmach",
                DateTime.Parse("15-7-1994"),
                Gender.Male,
                "0507664332",
                new Address("Hagilgal", 7, "Ramat-Gan"),
                VehicleType.HeavyTrack,
                GearBox.Automatic,
                "Stam-Beam",
                "Yossi-GevGever",
                44));
            TraineeList.Add(new Trainee(
                13579043,
                "Sechter",
                "Avner", 
                DateTime.Parse("10/11/1987"), 
                Gender.Male, 
                "054841234", 
                new Address("Mezada", 20, "Tel-Aviv"), 
                VehicleType.TwoWheeled, 
                GearBox.Manual, 
                "Mivhar", 
                "Ofer", 30));
            TraineeList.Add(new Trainee(
                99999999,
                "Navve",
                "Ofir",
                DateTime.Parse("31-12-1999"),
                Gender.Male,
                "0557667667",
                new Address("Dory", 30, "Raanana"),
                VehicleType.TwoWheeled,
                GearBox.Automatic,
                "Mem-Mem-Mem",
                "Tzion-Paz",
                29));
            #endregion
        }
    }
}
