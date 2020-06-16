using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    public static class ExtensionMethods
    {
        private static DL.IDL dl = DL.Factory.GetDL("xml");

        #region System-Time Extension
        public static DateTime SystemNow(this DateTime dateTime)
        {
           
            DateTime systemTime = DateTime.Now.AddDays(BLImplementation.SystemTimeFromRealTimeSpan.Days);
            return systemTime;
        }
        #endregion
        #region Enum Extensions
        public static string ToUserString(this VehicleType vehicleType)
        {
            string result = "";
            switch (vehicleType)
            {
                case VehicleType.TwoWheeled:
                    result = "Two Wheeled";
                    break;
                case VehicleType.Private:
                    result = "Private";
                    break;
                case VehicleType.LightTrack:
                    result = "Light Track";
                    break;
                case VehicleType.HeavyTrack:
                    result = "Heavy Track";
                    break;
            }
            return result;
        }
        #endregion
        #region Address Extensions
        public static DO.Address ToDOAddress(this Address boAddressSource)
        {
            return new DO.Address(
                boAddressSource.Street,
                boAddressSource.HouseNumber,
                boAddressSource.City);
        }
        public static Address ToBOAddress(this DO.Address doAddressSource)
        {
            return new Address(
                doAddressSource.Street,
                doAddressSource.HouseNumber,
                doAddressSource.City);
        }
        #endregion
        #region Trainee Extensions
        public static DO.Trainee ToDOTrainee(this Trainee BOTrainee)
        {
            DO.Trainee converted = new DO.Trainee(
                BOTrainee.ID,
                BOTrainee.LastName,
                BOTrainee.FirstName,
                BOTrainee.DateOfBirth,
                (DO.Gender)BOTrainee.Gender,
                BOTrainee.Phone,
                BOTrainee.Address.ToDOAddress(),
                (DO.VehicleType)BOTrainee.CarType,
                (DO.GearBox)BOTrainee.GearBoxType,
                BOTrainee.DrivingSchoolName,
                BOTrainee.TeacherName,
                BOTrainee.NumberOfLessons);
            return converted;
        }

        public static Trainee ToBOTrainee(this DO.Trainee DOTraineeSource)
        {

            Trainee converted = new Trainee(
                DOTraineeSource.ID,
                DOTraineeSource.LastName,
                DOTraineeSource.FirstName,
                DOTraineeSource.DateOfBirth,
                (Gender)DOTraineeSource.Gender,
                DOTraineeSource.Phone,
                DOTraineeSource.Address.ToBOAddress(),
                (VehicleType)DOTraineeSource.CarType,
                (GearBox)DOTraineeSource.GearBoxType,
                DOTraineeSource.DrivingSchoolName,
                DOTraineeSource.TeacherName,
                DOTraineeSource.NumberOfLessons,
                new List<TraineeTest>());
            return converted;
       
        }

        public static int CalculateAge(this Trainee trainee)
        {
            int age = new DateTime().SystemNow().Year - trainee.DateOfBirth.Year;

            //in case this-year's birthday hasn't come yet. He is 34.5, for example.
            if (new DateTime().SystemNow().AddYears(-age) < trainee.DateOfBirth)
            {
                return --age;
            }
            else return age;
        }

        public static int TestCount(this Trainee trainee)
        {
            int testsCount = 0;
            if (trainee.Tests.Any())
            {
                foreach (TraineeTest test in trainee.Tests)
                {
                    if (test.TestDate.AddHours(test.TestTime) < new DateTime().SystemNow())
                        testsCount++;
                }
            }
            return testsCount;
        }
        #endregion
        #region Tester Extensions

        public static DO.Tester ToDOTester(this Tester BOTester)
        {
            DO.Tester converted = new DO.Tester(
                BOTester.ID,
                BOTester.LastName,
                BOTester.FirstName,
                BOTester.DateOfBirth,
                (DO.Gender)BOTester.Gender,
                BOTester.Phone,
                BOTester.Address.ToDOAddress(),
                BOTester.MaxTestsPerWeek,
                BOTester.Seniority,
                (DO.VehicleType)BOTester.CarType,
                BOTester.MaxDistanceFromAddress);
            return converted;
        }


        public static Tester ToBOTester(this DO.Tester DOTester)
        {

            Tester converted = new Tester(
                DOTester.ID,
                DOTester.LastName,
                DOTester.FirstName,
                DOTester.DateOfBirth,
                (Gender)DOTester.Gender,
                DOTester.Phone,
                DOTester.Address.ToBOAddress(),
                DOTester.MaxTestsPerWeek,
                DOTester.Seniority,
                (VehicleType)DOTester.CarType,
                DOTester.MaxDistanceFromAddress,
                new List<TesterTest>());
            return converted;
        }

        public static int CalculateAge(this Tester tester)
        {
            int age = new DateTime().SystemNow().Year;
                age = age - tester.DateOfBirth.Year;
            //in case this-year's birthday hasn't come yet. He is 34.5, for example.
            if (new DateTime().SystemNow().AddYears(-age) < tester.DateOfBirth)
            {
                return --age;
            }
            else return age;
        }
        #endregion
        #region Test Extensions
        public static DO.Test ToDOTest(this Test BOTest)
        {
            DO.Test converted = new DO.Test(
                BOTest.TestNumber,
                BOTest.Tester.ID,
                BOTest.TraineeID,
                BOTest.TestDate,
                BOTest.TestTime,
                BOTest.ExitAddress.ToDOAddress(),
                BOTest.TestAlreadyDoneAndSealed,
                BOTest.DistanceKeeping,
                BOTest.ReverseParking,
                BOTest.LookingAtMirrors,
                BOTest.SignalsUsage,
                BOTest.PriorityGiving,
                BOTest.SpeedKeeping,
                BOTest.TestScore,
                BOTest.TestersNote
                );
            return converted;
        }

        public static DO.Test ToDOTest(this TesterTest BOTesterTest, int testerID)
        {
            DO.Test converted = new DO.Test(
                BOTesterTest.TestNumber,
                testerID,
                BOTesterTest.TraineeID,
                BOTesterTest.TestDate,
                BOTesterTest.TestTime,
                BOTesterTest.ExitAddress.ToDOAddress(),
                BOTesterTest.TestAlreadyDoneAndSealed,
                BOTesterTest.DistanceKeeping,
                BOTesterTest.ReverseParking,
                BOTesterTest.LookingAtMirrors,
                BOTesterTest.SignalsUsage,
                BOTesterTest.PriorityGiving,
                BOTesterTest.SpeedKeeping,
                BOTesterTest.TestScore,
                BOTesterTest.TestersNote
                );
            return converted;
        }

        public static Test ToBOTest(this DO.Test DOTest)
        {
            Tester tester = dl.getTester(DOTest.TesterID).ToBOTester();
            Trainee trainee = dl.getTrainee(DOTest.TraineeID).ToBOTrainee();
            Test converted = new Test(
                DOTest.TestNumber,
                new ExternalTester(tester),
                DOTest.TraineeID,
                trainee.FirstName + " " + trainee.LastName,
                DOTest.TestDate,
                DOTest.TestTime,
                DOTest.ExitAddress.ToBOAddress(),
                DOTest.TestAlreadyDoneAndSealed,
                DOTest.DistanceKeeping,
                DOTest.ReverseParking,
                DOTest.LookingAtMirrors,
                DOTest.SignalsUsage,
                DOTest.PriorityGiving,
                DOTest.SpeedKeeping,
                DOTest.TestScore,
                DOTest.TestersNote);
            return converted;
        }

        public static double PositiveFeedbackGradesPercentage(this Test test)
        {
            int numOfCreteriaTested = 6;
            int positiveGradesCount =
                Convert.ToInt32(test.DistanceKeeping)
                + Convert.ToInt32(test.ReverseParking)
                + Convert.ToInt32(test.LookingAtMirrors)
                + Convert.ToInt32(test.SignalsUsage)
                + Convert.ToInt32(test.PriorityGiving)
                + Convert.ToInt32(test.SpeedKeeping);
            return positiveGradesCount * (100 / numOfCreteriaTested);
        }
        
        #endregion
        #region TraineeTest & TesterTest Extensions
        public static double PositiveFeedbackGradesPercentage(this TraineeTest test)
        {
            int numOfCreteriaTested = 6;
            int positiveGradesCount =
                Convert.ToInt32(test.DistanceKeeping)
                + Convert.ToInt32(test.ReverseParking)
                + Convert.ToInt32(test.LookingAtMirrors)
                + Convert.ToInt32(test.SignalsUsage)
                + Convert.ToInt32(test.PriorityGiving)
                + Convert.ToInt32(test.SpeedKeeping);
            return positiveGradesCount * (100 / numOfCreteriaTested);
        }

        public static double PositiveFeedbackGradesPercentage(this TesterTest test)
        {
            int numOfCreteriaTested = 6;
            int positiveGradesCount =
                Convert.ToInt32(test.DistanceKeeping)
                + Convert.ToInt32(test.ReverseParking)
                + Convert.ToInt32(test.LookingAtMirrors)
                + Convert.ToInt32(test.SignalsUsage)
                + Convert.ToInt32(test.PriorityGiving)
                + Convert.ToInt32(test.SpeedKeeping);
            return positiveGradesCount * (100 / numOfCreteriaTested);
        }
        #endregion
        #region Exceptions Casting
        public static Exception ToBOException(this Exception exception)
        {
            if (exception is DO.TestersIdNotFoundException)
                return new Exception(((DO.TestersIdNotFoundException)exception).ToString());
            if (exception is DO.TraineesIdNotFoundException)
                return new Exception(((DO.TraineesIdNotFoundException)exception).ToString());
            if (exception is DO.TestersIdAlreadyExistsException)
                return new Exception(((DO.TestersIdAlreadyExistsException)exception).ToString());
            if (exception is DO.TraineesIdAlreadyExistsException)
                return new Exception(((DO.TraineesIdAlreadyExistsException)exception).ToString());
            if (exception is DO.TestNumberAlreadyExistsException)
                return new Exception(((DO.TestNumberAlreadyExistsException)exception).ToString());
            if (exception is DO.TestNumberNotFoundException)
                return new Exception(((DO.TestNumberNotFoundException)exception).ToString());
            if (exception is DO.TestNumberArgumentWasNotZeroException)
                return new Exception(((DO.TestNumberArgumentWasNotZeroException)exception).ToString());
            if (exception is DO.ConfigurationValueNotWriteableException)
                return new Exception(((DO.ConfigurationValueNotWriteableException)exception).ToString());
            if (exception is DO.NoMutchingImplementationException)
                return new Exception(((DO.NoMutchingImplementationException)exception).ToString());
            if (exception is DO.ScheduleWasNotInsertedException)
                return new Exception(((DO.ScheduleWasNotInsertedException)exception).ToString());
            if (exception is DO.ConfigurationValueNotExistException)
                return new Exception(((DO.ConfigurationValueNotExistException)exception).ToString());
            return exception;
        }
        #endregion
    }
}
