using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class TestersIdNotFoundException : Exception
    {
        public TestersIdNotFoundException() : base() { }
        public TestersIdNotFoundException(string message) : base(message) { }
        public TestersIdNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public TestersIdNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public TestersIdNotFoundException(int id) : base("the ID number:" + id.ToString() + "does not exist in the testers data base.\n")
        { }

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
        { }

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
        { }

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
        { }

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
        { }

        public override string ToString()
        {
            return "TestNumberAlreadyExistsException: " + Message;
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
        { }

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
        { }

        public override string ToString()
        {
            return "ConfigurationValueNotWriteableException: " + Message;
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
            return "NoMutchingImplementationException: " + Message;
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
    [Serializable]
    public class TestersDataSourceIsEmptyException : Exception
    {
        public TestersDataSourceIsEmptyException() : base() { }
        public TestersDataSourceIsEmptyException(string message) : base(message) { }
        public TestersDataSourceIsEmptyException(string message, Exception innerException) : base(message, innerException) { }
        public TestersDataSourceIsEmptyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "TestersDataSourceIsEmptyException: " + Message;
        }
    }
    [Serializable]
    public class TraineesDataSourceIsEmptyException : Exception
    {
        public TraineesDataSourceIsEmptyException() : base() { }
        public TraineesDataSourceIsEmptyException(string message) : base(message) { }
        public TraineesDataSourceIsEmptyException(string message, Exception innerException) : base(message, innerException) { }
        public TraineesDataSourceIsEmptyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "TraineesDataSourceIsEmptyException: " + Message;
        }
    }
}


