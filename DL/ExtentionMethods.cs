using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Reflection;
using System.Xml.Linq;

namespace DL
{
    static internal class ExtentionMethods
    {
        static internal Trainee Clone(this Trainee sourceTrainee)
        {
            Trainee copy = new Trainee(
                sourceTrainee.ID,
                sourceTrainee.LastName,
                sourceTrainee.FirstName,
                sourceTrainee.DateOfBirth,
                sourceTrainee.Gender,
                sourceTrainee.Phone,
                sourceTrainee.Address,
                sourceTrainee.CarType,
                sourceTrainee.GearBoxType,
                sourceTrainee.DrivingSchoolName,
                sourceTrainee.TeacherName,
                sourceTrainee.NumberOfLessons);
            return copy;
        }

        static internal Tester Clone(this Tester sourceTester)
        {

            Tester copy = new Tester(
                sourceTester.ID,
                sourceTester.LastName,
                sourceTester.FirstName,
                sourceTester.DateOfBirth,
                sourceTester.Gender,
                sourceTester.Phone,
                sourceTester.Address,
                sourceTester.Seniority,
                sourceTester.MaxTestsPerWeek,
                sourceTester.CarType,
                sourceTester.MaxDistanceFromAddress);
            return copy;
        }

        internal static Test Clone(this Test sourceTest)
        {
            Test copy = new Test(
                sourceTest.TestNumber,
                sourceTest.TesterID,
                sourceTest.TraineeID,
                sourceTest.TestDate,
                sourceTest.TestTime,
                sourceTest.ExitAddress,
                sourceTest.TestAlreadyDoneAndSealed,
                sourceTest.DistanceKeeping,
                sourceTest.ReverseParking,
                sourceTest.LookingAtMirrors,
                sourceTest.SignalsUsage,
                sourceTest.PriorityGiving,
                sourceTest.SpeedKeeping,
                sourceTest.TestScore,
                sourceTest.TestersNote);
            return copy;
        }

        internal static Trainee XmlToDOTrainee(this XElement traineeXElement)
        {
            var x = new Trainee(
                int.Parse(traineeXElement.Element("ID").Value),
                traineeXElement.Element("LastName").Value,
                traineeXElement.Element("FirstName").Value,
                DateTime.ParseExact(traineeXElement.Element("DateOfBirth").Value, "dd/MM/yyyy", null),
                (Gender)Enum.Parse(typeof(Gender), traineeXElement.Element("Gender").Value),
                traineeXElement.Element("Phone").Value,
                new Address(
                    traineeXElement.Element("Address").Element("Street").Value,
                    int.Parse(traineeXElement.Element("Address").Element("HouseNumber").Value),
                    traineeXElement.Element("Address").Element("City").Value),
                (VehicleType)Enum.Parse(typeof(VehicleType), traineeXElement.Element("CarType").Value),
                (GearBox)Enum.Parse(typeof(GearBox), traineeXElement.Element("GearBoxType").Value),
                traineeXElement.Element("DrivingSchoolName").Value,
                traineeXElement.Element("TeacherName").Value,
                int.Parse(traineeXElement.Element("NumberOfLessons").Value));
            return x;

        }
        internal static Tester XmlToDOTester(this XElement testerXElement)
        {
            return new Tester(
                int.Parse(testerXElement.Element("ID").Value),
                testerXElement.Element("LastName").Value,
                testerXElement.Element("FirstName").Value,
                DateTime.ParseExact(testerXElement.Element("DateOfBirth").Value, "dd/MM/yyyy", null),
                (Gender)Enum.Parse(typeof(Gender), testerXElement.Element("Gender").Value),
                testerXElement.Element("Phone").Value,
                new Address(
                    testerXElement.Element("Address").Element("Street").Value,
                    int.Parse(testerXElement.Element("Address").Element("HouseNumber").Value),
                    testerXElement.Element("Address").Element("City").Value),
                int.Parse(testerXElement.Element("Seniority").Value),
                int.Parse(testerXElement.Element("MaxTestsPerWeek").Value),
                (VehicleType)Enum.Parse(typeof(VehicleType), testerXElement.Element("CarType").Value),
                double.Parse(testerXElement.Element("MaxDistanceFromAddress").Value));
        }
        internal static Test XmlToDOTest(this XElement testXElement)
        {
            return new Test(
                int.Parse(testXElement.Element("TestNumber").Value),
                int.Parse(testXElement.Element("TesterID").Value),
                int.Parse(testXElement.Element("TraineeID").Value),
                DateTime.ParseExact(testXElement.Element("TestDate").Value, "dd/MM/yyyy", null),
                int.Parse(testXElement.Element("TestTime").Value),
                new Address(
                    testXElement.Element("ExitAddress").Element("Street").Value,
                    int.Parse(testXElement.Element("ExitAddress").Element("HouseNumber").Value),
                    testXElement.Element("ExitAddress").Element("City").Value),
                bool.Parse(testXElement.Element("TestAlreadyDoneAndSealed").Value),
                bool.Parse(testXElement.Element("DistanceKeeping").Value),
                bool.Parse(testXElement.Element("ReverseParking").Value), 
                bool.Parse(testXElement.Element("LookingAtMirrors").Value),
                bool.Parse(testXElement.Element("SignalsUsage").Value),
                bool.Parse(testXElement.Element("PriorityGiving").Value),
                bool.Parse(testXElement.Element("SpeedKeeping").Value),
                bool.Parse(testXElement.Element("TestScore").Value),
                testXElement.Element("TestersNote").Value);
        }
        internal static bool[,] XmlToDOSchedule(this XElement scheduleXElement)
        {
            string[] nameOfDays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday" };
            string[] nameOfHour = { "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen" };
            bool[,] schedule = new bool[5, 6];
            for(int i=0;i<5;i++)
            {
                for(int j=0;j<6;j++)
                {
                    schedule[i, j] = bool.Parse(scheduleXElement.Element(nameOfDays[i]).Element(nameOfHour[j]).Value);
                }
            }
            return schedule;
        }

        internal static XElement ToXmlTest(this Test test)
        {

            XElement TestNumber = new XElement("TestNumber", test.TestNumber.ToString());
            XElement TesterID = new XElement("TesterID", test.TesterID.ToString());
            XElement TraineeID = new XElement("TraineeID", test.TraineeID.ToString());
            XElement TestDate = new XElement("TestDate", test.TestDate.ToString("dd/MM/yyyy"));
            XElement TestTime = new XElement("TestTime", test.TestTime.ToString());
            XElement City = new XElement("City", test.ExitAddress.City);
            XElement Street = new XElement("Street", test.ExitAddress.Street);
            XElement HouseNumber = new XElement("HouseNumber", test.ExitAddress.HouseNumber);
            XElement ExitAddress = new XElement("ExitAddress", City, Street, HouseNumber);
            XElement TestAlreadyDoneAndSealed = new XElement("TestAlreadyDoneAndSealed", test.TestAlreadyDoneAndSealed.ToString());
            XElement DistanceKeeping = new XElement("DistanceKeeping", test.DistanceKeeping.ToString());
            XElement ReverseParking = new XElement("ReverseParking", test.ReverseParking.ToString());
            XElement LookingAtMirrors = new XElement("LookingAtMirrors", test.LookingAtMirrors.ToString());
            XElement SignalsUsage = new XElement("SignalsUsage", test.SignalsUsage.ToString());
            XElement PriorityGiving = new XElement("PriorityGiving", test.PriorityGiving.ToString());
            XElement SpeedKeeping = new XElement("SpeedKeeping", test.SpeedKeeping.ToString());
            XElement TestScore = new XElement("TestScore", test.TestScore.ToString());
            XElement TestersNote = new XElement("TestersNote", test.TestersNote);

            XElement testElement = new XElement("test", TestNumber, TesterID, TraineeID, TestDate, TestTime, ExitAddress, TestAlreadyDoneAndSealed,
                DistanceKeeping, ReverseParking, LookingAtMirrors, SignalsUsage, PriorityGiving, SpeedKeeping, TestScore, TestersNote);
            return testElement;
        }
        internal static XElement ToXmlTester(this Tester tester)
        {
            XElement ID = new XElement("ID", tester.ID);
            XElement LastName = new XElement("LastName", tester.LastName);
            XElement FirstName = new XElement("FirstName", tester.FirstName);
            XElement DateOfBirth = new XElement("DateOfBirth", tester.DateOfBirth.ToString("dd/MM/yyyy"));
            XElement Gender = new XElement("Gender", tester.Gender.ToString());
            XElement Phone = new XElement("Phone", tester.Phone);
            XElement City = new XElement("City", tester.Address.City);
            XElement Street = new XElement("Street", tester.Address.Street);
            XElement HouseNumber = new XElement("HouseNumber", tester.Address.HouseNumber);
            XElement Address = new XElement("Address", City, Street, HouseNumber);
            XElement CarType = new XElement("CarType", tester.CarType.ToString());
            XElement Seniority = new XElement("Seniority", tester.Seniority);
            XElement MaxTestsPerWeek = new XElement("MaxTestsPerWeek", tester.MaxTestsPerWeek);
            XElement MaxDistanceFromAddress = new XElement("MaxDistanceFromAddress", tester.MaxDistanceFromAddress);

            XElement testerElement = new XElement("tester", ID, LastName, FirstName, DateOfBirth, Gender, Phone, Address, CarType, Seniority, MaxTestsPerWeek, MaxDistanceFromAddress);
            return testerElement;
        }
        internal static XElement ToXmlSchedule (this bool[,] schedule)
        {
            XElement scheduleElement = new XElement("Schedule");
            string[] nameOfDays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday" };
            string[] nameOfHour = { "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen" };
            for (int i = 0; i < 5; i++)
            {
                XElement day = new XElement(nameOfDays[i]);
                for (int j = 0; j < 6; j++)
                {
                    XElement hour = new XElement(nameOfHour[j], schedule[i, j]);
                    day.Add(hour);
                }
                scheduleElement.Add(day);
            }
            return scheduleElement;
        }
        internal static XElement ToXmlTrainee(this Trainee trainee)
        {

            XElement ID = new XElement("ID", trainee.ID);
            XElement LastName = new XElement("LastName", trainee.LastName);
            XElement FirstName = new XElement("FirstName", trainee.FirstName);
            XElement DateOfBirth = new XElement("DateOfBirth", trainee.DateOfBirth.ToString("dd/MM/yyyy"));
            XElement Gender = new XElement("Gender", trainee.Gender.ToString());
            XElement Phone = new XElement("Phone", trainee.Phone);
            XElement City = new XElement("City", trainee.Address.City);
            XElement Street = new XElement("Street", trainee.Address.Street);
            XElement HouseNumber = new XElement("HouseNumber", trainee.Address.HouseNumber);
            XElement Address = new XElement("Address", City, Street, HouseNumber);
            XElement CarType = new XElement("CarType", trainee.CarType.ToString());
            XElement GearBoxType = new XElement("GearBoxType", trainee.GearBoxType.ToString());
            XElement DrivingSchoolName = new XElement("DrivingSchoolName", trainee.DrivingSchoolName);
            XElement TeacherName = new XElement("TeacherName", trainee.TeacherName);
            XElement NumberOfLessons = new XElement("NumberOfLessons", trainee.NumberOfLessons);

            XElement traineeElement = new XElement("trainee", ID, LastName, FirstName, DateOfBirth, Gender, Phone, Address, CarType, GearBoxType, DrivingSchoolName, TeacherName, NumberOfLessons);
            return traineeElement;
        }
    }
}
