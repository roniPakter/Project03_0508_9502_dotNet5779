using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DL
{
    internal class DLObject : IDL
    {
        //Singleton Method initialization
        private DLObject()
        {
            dataSorce = new DataSource();
            //Initialize the test number
            staticTestCode = (int)dataSorce.Configuration["BeginningSerialTestNumber"].Value;
        }
        private static DLObject instance = null;
        public static DLObject getInstance()
        {
            if (instance == null)
                instance = new DLObject();
            return instance;
        }

        public DataSource dataSorce;
        private static int staticTestCode;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        #region Create Implementations
        /// <summary>
        /// A function that orders a student at his request
        /// </summary>
        /// <param name="testToAdd"></param>
        /// <returns></returns>
        int IDL.addTest(Test testToAdd)
        {
            //Case shot and test number sent not zero
            if (testToAdd.TestNumber != 0)
            {
                throw new TestNumberArgumentWasNotZeroException();
            }
            //Check if the testeer's ID number matches the data we have
            bool noSuchID = true;
            foreach (Tester tester in DataSource.TesterList)
            {
                if (tester.ID == testToAdd.TesterID)
                    noSuchID = false;
            }
            if (noSuchID)
                throw new TestersIdNotFoundException(testToAdd.TesterID);
            //Check if the student's ID number matches the data we have
            noSuchID = true;
            foreach (Trainee trainee in DataSource.TraineeList)
            {
                if (trainee.ID == testToAdd.TraineeID)
                    noSuchID = false;
            }
            if (noSuchID)
                throw new TraineesIdNotFoundException(testToAdd.TraineeID);
            //Add the test to the general test list
            DataSource.TestList.Add(new Test(
                staticTestCode,
                testToAdd.TesterID,
                testToAdd.TraineeID,
                testToAdd.TestDate,
                testToAdd.TestTime,
                testToAdd.ExitAddress,
                testToAdd.TestAlreadyDoneAndSealed,
                testToAdd.DistanceKeeping,
                testToAdd.ReverseParking,
                testToAdd.LookingAtMirrors,
                testToAdd.SignalsUsage,
                testToAdd.PriorityGiving,
                testToAdd.SpeedKeeping,
                testToAdd.TestScore,
                testToAdd.TestersNote));
            //Updating the test number
            staticTestCode++;
            return staticTestCode - 1;
        }

        /// <summary>
        /// A function that adds a tester, the function receives the tester information and its working times
        /// </summary>
        /// <param name="testerToAdd"></param>
        /// <param name="schedule"></param>
        void IDL.addTester(Tester testerToAdd, bool[,] schedule)
        {
            // Exception in case ID already exists in the system
            foreach (Tester tester in DataSource.TesterList)
                if (tester.ID == testerToAdd.ID)
                    throw new TestersIdAlreadyExistsException(testerToAdd.ID, tester.FirstName + " " + tester.LastName);

            DataSource.TesterList.Add(testerToAdd);
            dataSorce.Schedules.Add(testerToAdd.ID, schedule);
        }

        /// <summary>
        /// A function added by a trainee
        /// </summary>
        /// <param name="trainee"></param>
        void IDL.addTrainee(Trainee trainee)
        {
            // Exception in case ID already exists in the system
            foreach (Trainee _trainee in DataSource.TraineeList)
                if (_trainee.ID == trainee.ID)
                    throw new TraineesIdAlreadyExistsException(trainee.ID, _trainee.FirstName + " " + _trainee.LastName);
            DataSource.TraineeList.Add(trainee.Clone());
        }
        #endregion

        #region Request Implementations: Single Objects
        /// <summary>
        /// A function that returns a test
        /// </summary>
        /// <param name="testNumber"></param>
        /// <returns></returns>
        Test IDL.getTest(int testNumber)
        {
            foreach (Test test in DataSource.TestList)
            {
                //If there is such a test in the system, the function will return the test, otherwise it will throw an exception.
                if (test.TestNumber == testNumber)
                    return test.Clone();//Deep copying
            }
            throw new TestNumberNotFoundException(testNumber);
        }

        /// <summary>
        /// get a specific tester by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Tester IDL.getTester(int id)
        {
            foreach (Tester tester in DataSource.TesterList)
            {
                //If there is such a tester in the system, the function will return the tester, otherwise it will throw an exception.
                if (tester.ID == id)
                    return tester.Clone();
            }
            throw new TestersIdNotFoundException(id);
        }

        /// <summary>
        /// get a specific trainee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Trainee IDL.getTrainee(int id)
        {
            foreach (Trainee trainee in DataSource.TraineeList)
            {
                //If there is such a trainee in the system, the function will return the trainee, otherwise it will throw an exception.
                if (trainee.ID == id)
                    return trainee.Clone();
            }
            throw new TraineesIdNotFoundException(id);
        }
        #endregion

        #region Request Implementations: Certain Condition Lists
        /// <summary>
        /// A function that returns a list of testers that are compatible with the pred
        /// </summary>
        /// <param name="pred"></param>
        /// <returns></returns>
        List<Tester> IDL.getCertainTestersList(Predicate<Tester> pred) //Can be with LinQ 
        {
            List<Tester> listToReturn = new List<Tester>();
            //There is no such tester in the system
            if (!DataSource.TesterList.Any())
                throw new TestersDataSourceIsEmptyException("The Testers database is empty!");
            foreach (Tester tester in DataSource.TesterList)
            {
                //If the tester is compatible with the pred, Add it to the list
                if (pred(tester))
                    listToReturn.Add(tester.Clone());
            }
            return listToReturn;
        }

        /// <summary>
        /// A function that returns a list of tests that are compatible with the pred
        /// </summary>
        /// <param name="pred"></param>
        /// <returns></returns>
        List<Test> IDL.getCertainTestsList(Predicate<Test> pred)
        {
            List<Test> listToReturn = new List<Test>();
            foreach (Test test in DataSource.TestList)
            {
                //If the tests is compatible with the pred, Add it to the list
                if (pred(test))
                    listToReturn.Add(test.Clone());
            }
            return listToReturn;
        }

        /// <summary>
        /// A function that returns a list of trainee that are compatible with the pred
        /// </summary>
        /// <param name="pred"></param>
        /// <returns></returns>
        List<Trainee> IDL.getCertainTraineesList(Predicate<Trainee> pred)
        {
            //There is no such trainee in the system
            if (!DataSource.TraineeList.Any())
                throw new TraineesDataSourceIsEmptyException("The Trainees database is empty!");
            List<Trainee> listToReturn = new List<Trainee>();
            foreach (Trainee trainee in DataSource.TraineeList)
            {
                //If the trainee is compatible with the pred, Add it to the list
                if (pred(trainee))
                    listToReturn.Add(trainee.Clone());
            }
            return listToReturn;
        }
        #endregion
        
        #region Request Implementations: Whole Lists
        
         /// <summary>
        /// A function that returns the entire list of testers
        /// </summary>
        /// <returns></returns>
        List<Tester> IDL.getTesterList()
        {
            //Exceeded if the list is empty
            if (!DataSource.TesterList.Any())
                throw new TestersDataSourceIsEmptyException("The Testers database is empty!");
            return DataSource.TesterList.Select(tester => tester.Clone()).ToList();
            //return new List<Tester>(DataSource.TesterList);
        }

        /// <summary>
        /// A function that returns the entire list of tests
        /// </summary>
        /// <returns></returns>
        List<Test> IDL.getTestList()
        {
            return DataSource.TestList.Select(test => test.Clone()).ToList();
            //return new List<Test>(DataSource.TestList);
        }

        /// <summary>
        /// A function that returns the entire list of trainee
        /// </summary>
        /// <returns></returns>
        List<Trainee> IDL.getTraineeList()
        {
            if (!DataSource.TraineeList.Any())
                throw new TraineesDataSourceIsEmptyException("The Trainees database is empty!");
            return DataSource.TraineeList.Select(trainee => trainee.Clone()).ToList();
           
        }
        #endregion

       /// <summary>
       /// A function that returns the configuration of the system
       /// </summary>
       /// <returns></returns>
        Dictionary<string, object> IDL.getConfig()
        {
            //get the config from the datasource as a dictionary
            Dictionary<string, object> config =
                dataSorce.Configuration.Where
                (item => item.Value.Readable).ToDictionary
                (item => item.Key, item => item.Value.Value);
            return config;
        }

        /// <summary>
        /// A function that setts a specific configuration value
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="value"></param>
        void IDL.setConfig(string parm, object value)
        {
            //is the key exist?
            if (dataSorce.Configuration.ContainsKey(parm))
            {
                //is it writeable?
                if (dataSorce.Configuration[parm].Writeable)
                {
                    dataSorce.Configuration[parm].Value = value;
                }
                else
                    throw new ConfigurationValueNotWriteableException(parm);
            }
            else
                throw new ConfigurationValueNotExistException("the inserted value does not exist in the configuration");
        }

        #region Delete Implementations
        /// <summary>
        /// A function that deletes the test it received from the system
        /// </summary>
        /// <param name="testNumber"></param>
        void IDL.removeTest(int testNumber)
        {
            //Check if there is such a test in the system, otherwise an exception will be thrown
            foreach (Test test in DataSource.TestList)
            {
                if (test.TestNumber == testNumber)
                {
                    DataSource.TestList.Remove(test);
                    return;
                }
            }
            throw new TestNumberNotFoundException("thrown from: DL.DLObject.removeTest");
        }
        /// <summary>
        /// A function that deletes the tester it received from the system
        /// </summary>
        /// <param name="id"></param>
        void IDL.removeTester(int id)
        {
            //Check if there is such a tester in the system, otherwise an exception will be thrown
            foreach (Tester tester in DataSource.TesterList)
            {
                if (tester.ID == id)
                {
                    DataSource.TesterList.Remove(tester);
                    return;
                }
            }
            throw new TestersIdNotFoundException("thrown from: DL.DLObject.removeTester");
        }

        /// <summary>
        /// A function that deletes the trainee it received from the system
        /// </summary>
        /// <param name="id"></param>
        void IDL.removeTrainee(int id)
        {
            //Check if there is such a trainee in the system, otherwise an exception will be thrown
            foreach (Trainee trainee in DataSource.TraineeList)
            {
                if (trainee.ID == id)
                {
                    DataSource.TraineeList.Remove(trainee);
                    return;
                }
            }
            throw new TraineesIdNotFoundException("thrown from: DL.DLObject.removeTrainee");
        }
        #endregion


        #region Update Implementations
        /// <summary>
        /// A function that updates the test information, the function receives the test number and the updated test
        /// </summary>
        /// <param name="testNumber"></param>
        /// <param name="updatedTest"></param>
        void IDL.updateTest(int testNumber, Test updatedTest)
        {
            bool NoSuchTestNumber = true;
            foreach (Test test in DataSource.TestList)
            {
                //Check if there is such a test in the system, otherwise an exception will be thrown
                if (test.TestNumber == testNumber)
                {
                    NoSuchTestNumber = false;
                    //Remove the old test and add the updated  test
                    DataSource.TestList.Remove(test);
                    DataSource.TestList.Add(updatedTest.Clone());
                    break;
                }
            }
            if (NoSuchTestNumber)
                throw new TestNumberNotFoundException(testNumber);
        }
        /// <summary>
        ///  A function that updates the tester information, the function receives the tester ID and the updated tester
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedTester"></param>
        /// <param name="schedule"></param>
        void IDL.updateTester(int id, Tester updatedTester, bool[,] schedule)
        {
            //Check if there is such a tester in the system, otherwise an exception will be thrown
            bool NoSuchTestNumber = true;
            foreach (Tester tester in DataSource.TesterList)
            {
                if (tester.ID == id)
                {
                    NoSuchTestNumber = false;
                    //Remove the old tester and add the updated  tester
                    DataSource.TesterList.Remove(tester);
                    DataSource.TesterList.Add(updatedTester.Clone());

                    dataSorce.Schedules[id] = schedule;
                    break;
                }
            }
            if (NoSuchTestNumber)
                throw new TestersIdNotFoundException(id);
        }

        /// <summary>
        ///  A function that updates the trainee information, the function receives the trainee ID and the updated trainee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedTrainee"></param>
        void IDL.updateTrainee(int id, Trainee updatedTrainee)
        {
            bool noSuchTrainee = true;
            foreach (Trainee trainee in DataSource.TraineeList)
            {
                if (trainee.ID == id)
                {
                    noSuchTrainee = false;
                    //Remove the old trainee and add the updated  trainee
                    DataSource.TraineeList.Remove(trainee);
                    DataSource.TraineeList.Add(updatedTrainee.Clone());
                    break;
                }
            }
            if (noSuchTrainee)
                throw new TraineesIdNotFoundException(id);
        }
        #endregion

        /// <summary>
        ///  A function that returns the work time of the tester the function has received
        /// </summary>
        /// <param name="testerID"></param>
        /// <returns></returns>
        bool[,] IDL.getSchedule(int testerID)
        {
            //Check if there is such a tester in the system, otherwise an exception will be thrown
            if (dataSorce.Schedules.ContainsKey(testerID))
            {
                return dataSorce.Schedules[testerID];
            }
            else throw new ScheduleWasNotInsertedException("the tester's schedule was not inserted yet");
        }

    }

}

