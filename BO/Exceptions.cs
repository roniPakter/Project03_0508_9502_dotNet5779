using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class TestersIdNotFoundException : Exception
    {
        public TestersIdNotFoundException() : base() { }
        public TestersIdNotFoundException(string message) : base(message) { }
        public TestersIdNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public TestersIdNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public TestersIdNotFoundException(int id) : base("the ID number:" + id.ToString() + "does not exist in the testers data base.\n")
        {}

        public override string ToString()
        {
            return "TestersIdNotFoundException: " + Message;
        }
    }
    [Serializable]
    public class TraineesIdNotFoundException : Exception
    {
        public TraineesIdNotFoundException() : base() { }
        public TraineesIdNotFoundException(string message) : base(message) { }
        public TraineesIdNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public TraineesIdNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public TraineesIdNotFoundException(int id) : base("the ID number:" + id.ToString() + "does not exist in the trainees data base.\n")
        {}

        public override string ToString()
        {
            return "TraineesIdNotFoundException: " + Message;
        }
    }
    [Serializable]
    public class TestersIdAlreadyExistsException : Exception
    {
        public TestersIdAlreadyExistsException() : base() { }
        public TestersIdAlreadyExistsException(string message) : base(message) { }
        public TestersIdAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
        public TestersIdAlreadyExistsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public TestersIdAlreadyExistsException(int id, string name) : base("The ID number: " + id.ToString() + "already exists in the testers data base, under the name: " + name + ".\n")
        {}

        public override string ToString()
        {
            return "TestersIdAlreadyExistsException: " + Message;
        }
    }
    [Serializable]
    public class TraineesIdAlreadyExistsException : Exception
    {
        public TraineesIdAlreadyExistsException() : base() { }
        public TraineesIdAlreadyExistsException(string message) : base(message) { }
        public TraineesIdAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
        public TraineesIdAlreadyExistsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public TraineesIdAlreadyExistsException(int id, string name) : base("The ID number:" + id.ToString() + "already exists in the testers data base, under the name:" + name + ".\n")
        {}

        public override string ToString()
        {
            return "TraineesIdAlreadyExistsException: " + Message;
        }
    }
    [Serializable]
    public class TestNumberAlreadyExistsException : Exception
    {
        public TestNumberAlreadyExistsException() : base() { }
        public TestNumberAlreadyExistsException(string message) : base(message) { }
        public TestNumberAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
        public TestNumberAlreadyExistsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public TestNumberAlreadyExistsException(int testNumber) : base("the TestNumber:" + testNumber.ToString() + "already exists in the test data base.\n")
        {}

        public override string ToString()
        {
            return "TestNumberAlreadyExistsException: "+ Message;
        }
    }
    [Serializable]
    public class TestNumberNotFoundException : Exception
    {
        public TestNumberNotFoundException() : base() { }
        public TestNumberNotFoundException(string message) : base(message) { }
        public TestNumberNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public TestNumberNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public TestNumberNotFoundException(int testNumber) : base("the TestNumber:" + testNumber.ToString() + "does not exist in the test data base.\n")
        {}

        public override string ToString()
        {
            return "TestNumberNotFoundException: " + Message;
        }
    }
    [Serializable]
    public class TestNumberArgumentWasNotZeroException : Exception
    {
        public TestNumberArgumentWasNotZeroException() : base("The TestNumber cannot be passed to the data layer as argument") { }
        public TestNumberArgumentWasNotZeroException(string message) : base(message) { }
        public TestNumberArgumentWasNotZeroException(string message, Exception innerException) : base(message, innerException) { }
        public TestNumberArgumentWasNotZeroException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return "TestNumberArgumentWasNotNullException: " + Message;
        }
    }
    [Serializable]
    public class ConfigurationValueNotWriteableException : Exception
    {
        public ConfigurationValueNotWriteableException() : base() { }
        public ConfigurationValueNotWriteableException(string message, Exception innerException) : base(message, innerException) { }
        public ConfigurationValueNotWriteableException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public ConfigurationValueNotWriteableException(string key) : base("The key asked to update in the configuration: " + key + " - is forbidden to write ")
        {}

        public override string ToString()
        {
            return "ConfigurationValueNotWriteableException: "+ Message;
        }
    }
    [Serializable]
    public class NoMutchingImplementationException : Exception
    {
        public NoMutchingImplementationException() : base() { }
        public NoMutchingImplementationException(string message, Exception innerException) : base(message, innerException) { }
        public NoMutchingImplementationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public NoMutchingImplementationException(string notImplementedType) : base("No DL implementaion exists for the type: " + notImplementedType + "\n")
        { }

        public override string ToString()
        {
            return "NoMutchingImplementationException: "+ Message;
        }
    }
    
    [Serializable]
    public class ScheduleWasNotInsertedException : Exception
    {
        public ScheduleWasNotInsertedException() : base() { }
        public ScheduleWasNotInsertedException(string message) : base(message) { }
        public ScheduleWasNotInsertedException(string message, Exception innerException) : base(message, innerException) { }
        public ScheduleWasNotInsertedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "ScheduleWasNotInsertedException: " + Message;
        }
    }
    [Serializable]
    public class ConfigurationValueNotExistException : Exception
    {
        public ConfigurationValueNotExistException() : base() { }
        public ConfigurationValueNotExistException(string message) : base(message) { }
        public ConfigurationValueNotExistException(string message, Exception innerException) : base(message, innerException) { }
        public ConfigurationValueNotExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "ConfigurationValueNotExistException: " + Message;
        }
    }

    //unique to the BO:
    [Serializable]
    public class NoSuchIdException : Exception
    {
        public NoSuchIdException() : base() { }
        public NoSuchIdException(string message) : base(message) { }
        public NoSuchIdException(string message, Exception innerException) : base(message, innerException) { }
        public NoSuchIdException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "NoSuchIdException: " + Message;
        }
    }
    [Serializable]
    public class AskedDateIsNotAvailableException : Exception
    {
        public DateTime AlternativeDate;
        public int AlternativeTime;
        public Trainee Trainee;
        public Address ExitAddress;
        public Tester FoundTester;

        public AskedDateIsNotAvailableException() : base() { }
        public AskedDateIsNotAvailableException(string message) : base(message) { }
        public AskedDateIsNotAvailableException(string message, Exception innerException) : base(message, innerException) { }
        public AskedDateIsNotAvailableException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public AskedDateIsNotAvailableException( DateTime alternativeDate, int alternativeTime, Trainee trainee, Address exitAddress, Tester found, string message = "") : base(message)
        {
            AlternativeDate = alternativeDate;
            AlternativeTime = alternativeTime;
            Trainee = trainee;
            ExitAddress = exitAddress;
            FoundTester = found;
        }

        public override string ToString()
        {
            return "AskedDateIsNotAvailableException: " + Message;
        }
    }
    [Serializable]
    public class NotEnoughLessonsException : Exception
    {
        public NotEnoughLessonsException() : base() { }
        public NotEnoughLessonsException(string message) : base(message) { }
        public NotEnoughLessonsException(string message, Exception innerException) : base(message, innerException) { }
        public NotEnoughLessonsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public NotEnoughLessonsException(int lessonsNeeded) : base("The trainee needs to do " + lessonsNeeded + " more lessons before being tested.\n")
        {}

        public override string ToString()
        {
            return "NotEnoughLessonsException: "+ Message;
        }
    }
    [Serializable]
    public class TraineeIsWaitingForTestException : Exception
    {
        public TraineeIsWaitingForTestException() : base() { }
        public TraineeIsWaitingForTestException(string message) : base(message) { }
        public TraineeIsWaitingForTestException(string message, Exception innerException) : base(message, innerException) { }
        public TraineeIsWaitingForTestException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public TraineeIsWaitingForTestException(DateTime date, string message = "") : base(message + date.ToShortDateString())
        {}

        public override string ToString()
        {
            return "TraineeIsWaitingForTestException: " + Message ;
        }
    }
    [Serializable]
    public class HasPassedTestException : Exception
    {
        public HasPassedTestException() : base() { }
        public HasPassedTestException(string message) : base(message) { }
        public HasPassedTestException(string message, Exception innerException) : base(message, innerException) { }
        public HasPassedTestException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public HasPassedTestException(int testNum, string message) : base("The Trainee Has Already passed a test\n The successful tes number is: " + testNum.ToString())
        {}

        public override string ToString()
        {
            return "HasPassedTestException: " + Message;
        }
    }
    [Serializable]
    public class NotEnoughDaysIntervalException : Exception
    {
        public NotEnoughDaysIntervalException() : base() { }
        public NotEnoughDaysIntervalException(string message) : base(message) { }
        public NotEnoughDaysIntervalException(string message, Exception innerException) : base(message, innerException) { }
        public NotEnoughDaysIntervalException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public NotEnoughDaysIntervalException(DateTime earliestAllowedDate, string message = "") : base("Not enough days interval from the last test.\nPlease choose a date from the date: " + earliestAllowedDate.ToString("dd/MM/yyyy") + " and on.\n")
        {}

        public override string ToString()
        {
            return "NotEnoughDaysIntervalException: " + Message;
        }
    }
    [Serializable]
    public class NoTesterForVehicleException : Exception
    {
        public NoTesterForVehicleException() : base() { }
        public NoTesterForVehicleException(string message) : base(message) { }
        public NoTesterForVehicleException(string message, Exception innerException) : base(message, innerException) { }
        public NoTesterForVehicleException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "NoTesterForVehicleException: " + Message;
        }
    }
    [Serializable]
    public class NoTesterCloseEnoughException : Exception
    {
        public NoTesterCloseEnoughException() : base() { }
        public NoTesterCloseEnoughException(string message) : base(message) { }
        public NoTesterCloseEnoughException(string message, Exception innerException) : base(message, innerException) { }
        public NoTesterCloseEnoughException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return "NoTesterCloseEnoughException: " + Message;
        }
    }
    [Serializable]
    public class TooYoungeTesterException : Exception
    {
        public TooYoungeTesterException() : base() { }
        public TooYoungeTesterException(string message) : base(message) { }
        public TooYoungeTesterException(string message, Exception innerException) : base(message, innerException) { }
        public TooYoungeTesterException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "TooYoungeTesterException: " + Message;
        }
    }
    [Serializable]
    public class TooOldTesterException : Exception
    {
        public TooOldTesterException() : base() { }
        public TooOldTesterException(string message) : base(message) { }
        public TooOldTesterException(string message, Exception innerException) : base(message, innerException) { }
        public TooOldTesterException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "TooOldTesterException: " + Message;
        }
    }
    [Serializable]
    public class TooYoungeTraineeException : Exception
    {
        public TooYoungeTraineeException() : base() { }
        public TooYoungeTraineeException(string message) : base(message) { }
        public TooYoungeTraineeException(string message, Exception innerException) : base(message, innerException) { }
        public TooYoungeTraineeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "TooYoungeTraineeException: " + Message;
        }
    }
    [Serializable]
    public class InvalidTestNumberException : Exception
    {
        public InvalidTestNumberException() : base() { }
        public InvalidTestNumberException(string message) : base(message) { }
        public InvalidTestNumberException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidTestNumberException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "InvalidTestNumberException: " + Message;
        }
    }
    [Serializable]
    public class FeedbackBeforeTestEndsException : Exception
    {
        public FeedbackBeforeTestEndsException() : base() { }
        public FeedbackBeforeTestEndsException(string message) : base(message) { }
        public FeedbackBeforeTestEndsException(string message, Exception innerException) : base(message, innerException) { }
        public FeedbackBeforeTestEndsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "FeedbackBeforeTestEndsException: " + Message;
        }
    }
    [Serializable]
    public class TestHasBeenDoneAndSealedException : Exception
    {
        public TestHasBeenDoneAndSealedException() : base() { }
        public TestHasBeenDoneAndSealedException(string message) : base(message) { }
        public TestHasBeenDoneAndSealedException(string message, Exception innerException) : base(message, innerException) { }
        public TestHasBeenDoneAndSealedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "TestHasBeenDoneAndSealedException: " + Message;
        }
    }
    [Serializable]
    public class FeedbackMakesNoSenseException : Exception
    {
        public FeedbackMakesNoSenseException() : base() { }
        public FeedbackMakesNoSenseException(string message) : base(message) { }
        public FeedbackMakesNoSenseException(string message, Exception innerException) : base(message, innerException) { }
        public FeedbackMakesNoSenseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "FeedbackMakesNoSenseException: " + Message;
        }
    }
    [Serializable]
    public class InvalidAddressInsertedException : Exception
    {
        public InvalidAddressInsertedException() : base() { }
        public InvalidAddressInsertedException(string message) : base(message) { }
        public InvalidAddressInsertedException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidAddressInsertedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "InvalidAddressInsertedException: " + Message;
        }
    }
    [Serializable]
    public class InvalidDateForTestException : Exception
    {
        public InvalidDateForTestException() : base() { }
        public InvalidDateForTestException(string message) : base(message) { }
        public InvalidDateForTestException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidDateForTestException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "InvalidDateForTestException: " + Message;
        }
    }
    [Serializable]
    public class NetworkConnectionErrorException : Exception
    {
        public NetworkConnectionErrorException() : base() { }
        public NetworkConnectionErrorException(string message) : base(message) { }
        public NetworkConnectionErrorException(string message, Exception innerException) : base(message, innerException) { }
        public NetworkConnectionErrorException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "NetworkConnectionErrorException: " + Message;
        }
    }
    [Serializable]
    public class TestCannotBeDeletedException : Exception
    {
        public TestCannotBeDeletedException() : base() { }
        public TestCannotBeDeletedException(string message) : base(message) { }
        public TestCannotBeDeletedException(string message, Exception innerException) : base(message, innerException) { }
        public TestCannotBeDeletedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "TestCannotBeDeletedException: " + Message;
        }
    }
    [Serializable]
    public class SystemTimeSetToPastException : Exception
    {
        public SystemTimeSetToPastException() : base() { }
        public SystemTimeSetToPastException(string message) : base(message) { }
        public SystemTimeSetToPastException(string message, Exception innerException) : base(message, innerException) { }
        public SystemTimeSetToPastException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
            return "SystemTimeSetToPastException: " + Message;
        }
    }
    

}
