using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Globalization;
using System.Reflection;
using System.Net;
using System.IO;
using System.Xml;
using System.Threading;
using System.ComponentModel;


namespace BL
{
    
    public class BLImplementation : IBL
    {
        
        internal DL.IDL dlObject { get; set; }
        private Configuration Configuration { get; set; }
      
        internal static TimeSpan SystemTimeFromRealTimeSpan { get; set; }
       
        
        
        //singleton method:
        private BLImplementation()
        {
            dlObject = DL.Factory.GetDL("xml");
            Configuration = new Configuration();
            
            SystemTimeFromRealTimeSpan = TimeSpan.Zero;
           
        }
        private static BLImplementation instance = null;
        public static BLImplementation getInstance()
        {
            if (instance == null)
                instance = new BLImplementation();
            return instance;
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        #region Create Implementations
        /// <summary>
        /// A function adds a test to the student,
       /// The function gives a reference to BackgroundWorker, the current student, a desired date, a desired time, and a test exit address
        /// </summary>
        /// <param name="addTestWorker"></param>
        /// <param name="trainee"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="exitAddress"></param>
        void IBL.addTest(BackgroundWorker addTestWorker, Trainee trainee, DateTime date, int time, Address exitAddress)
        {
            try
            {
                if (date < new DateTime().SystemNow())
                    throw new InvalidDateForTestException("Setting a test to the past is impossible");
                if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Saturday)
                    throw new InvalidDateForTestException("A test may not be appointed to weekend!");
                //Check if the student is allowed to access the test
                checkIfTraineeIsAllowed(trainee);
                //Initial screening - build a list of testers that match the student's vehicle type
                List<DO.Tester> relevantCarTypeTesters = getRelevantTestersList(trainee.CarType);
                addTestWorker.DoWork += AddTestWorker_DoWork;
                addTestWorker.RunWorkerAsync(new List<Object> { exitAddress, relevantCarTypeTesters, date, time, trainee });
               
            }

            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
            
        }
        private void checkIfTraineeIsAllowed(Trainee trainee)
        {
            try
            {
                //learned enough lessons?
                if (trainee.NumberOfLessons < Configuration.MinimumNumberOfLessons)
                    throw new NotEnoughLessonsException(Configuration.MinimumNumberOfLessons - trainee.NumberOfLessons);
                //it is not his first test! hmmm...
                if (trainee.Tests.Any())
                {
                    //is he waiting for a test?
                    var inCompleteTestDate = from test in trainee.Tests
                                             where test.TestAlreadyDoneAndSealed == false
                                             select test.TestDate;
                    if (inCompleteTestDate.Any())
                        throw new TraineeIsWaitingForTestException(inCompleteTestDate.First(), "You cannot appoint a test before getting the results of the test from the date: ");
                    //has he passed a test already?
                    var successfulTest = from test in trainee.Tests
                                         where test.TestScore == true
                                         select test.TestNumber;
                    if (successfulTest.Any())
                        throw new HasPassedTestException(successfulTest.First(), "the trainee has already passed a test");
                    //enough time between the last test and the asked date?
                    DateTime lastTestDate = trainee.Tests.Max(test => test.TestDate);
                    if ((new DateTime().SystemNow() - lastTestDate).Days < Configuration.DaysIntervalBetweenTests)
                        throw new NotEnoughDaysIntervalException(lastTestDate.AddDays(Configuration.DaysIntervalBetweenTests));
                }
            }
            catch (Exception exception)
            {

                throw exception.ToBOException();
            }
        }      
        private List<DO.Tester> getRelevantTestersList(VehicleType carType)
        {
            try
            {
                List<DO.Tester> relevants = dlObject.getCertainTestersList(tester => tester.CarType == (DO.VehicleType)carType);
                //are there testers to the vehicle type?
                if (relevants.Any() == false)
                    throw new NoTesterForVehicleException("Sorry, we have no tester for this vehicle type.\n");
                return relevants;
                
            }
            catch (Exception exception)
            {

                throw exception.ToBOException();
            }
         }


        private void AddTestWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                List<Object> parms = e.Argument as List<Object>;
                Address exitAddress = (Address)parms[0];
                List<DO.Tester> relevants = parms[1] as List<DO.Tester>;
                DateTime date = (DateTime)parms[2];
                int time = (int)parms[3];
                Trainee trainee = (Trainee)parms[4];
                
                List<Tester> relevantAndCloseTesters = relevants.
                         Where(tester => distance(tester.Address.ToBOAddress(), exitAddress, worker) <= tester.MaxDistanceFromAddress).
                         Select(x => x.ToBOTester()).ToList();
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                foreach (Tester BOtester in relevantAndCloseTesters)
                {
                    addListOfTests(BOtester);
                    addTesterSchedule(BOtester);
                }

                if (relevantAndCloseTesters.Any() == false)
                    throw new NoTesterCloseEnoughException("Sorry, we have no tester close enough to the asked exit address, please choose another address.\n");
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                ExternalTester foundTester = getAvailableTester(relevantAndCloseTesters, date, time, trainee, exitAddress, worker);
                Test test = new Test(
                    0,
                    foundTester,
                    trainee.ID,
                    trainee.FirstName + " " + trainee.LastName,
                    date,
                    time,
                    exitAddress);
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                DO.Test testToAdd = test.ToDOTest();
                int fixedTestNumber = dlObject.addTest(testToAdd);
                trainee.Tests.Add(
                    new TraineeTest(dlObject.getTest(fixedTestNumber).ToBOTest(),
                    new ExternalTrainee(trainee)));
                e.Result = fixedTestNumber;
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    e.Cancel = true;
                }
                e.Result = ex.ToBOException();
            }
            
        }

        private double distance(Address original, Address destination, BackgroundWorker worker)
        {
            try
            {
                while(true)
                {
                    if (worker.CancellationPending)
                    {
                        throw new OperationCanceledException();
                    }
                    string exitString = original.ToString();
                    string destinationString = destination.ToString();

                    string KEY = @"G8FDvpx8mfhqoApeJm8u5HBak8NeaPRW";
                    string url = @"https://www.mapquestapi.com/directions/v2/route" +
                     @"?key=" + KEY +
                     @"&from=" + exitString +
                     @"&to=" + destinationString +
                     @"&outFormat=xml" +
                     @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
                     @"&enhancedNarrative=false&avoidTimedConditions=false";
                    //request from MapQuest service the distance between the 2 addresses
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader sreader = new StreamReader(dataStream);
                    string responsereader = sreader.ReadToEnd();
                    response.Close();

                    //the response is given in an XML format
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(responsereader);
                    if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
                    //we have the expected answer
                    {
                        //display the returned distance
                        XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                        double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                        return (distInMiles * 1.609344);
                    }
                    else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
                    //we have an answer that an error occurred, one of the addresses is not found
                    {
                        throw new InvalidAddressInsertedException("One of the addresses cannot be found. Check the addresses.");
                    }
                    else //busy network or other error...
                    {
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception exception)
            {

                throw exception.ToBOException();
            }
        }

        private ExternalTester getAvailableTester(List<Tester> testersList, DateTime date, int time, Trainee trainee, Address exitAddress, BackgroundWorker worker)
        {
            try
            {
                Tester found = testersList.Find(currentTester => isTesterAvailable(currentTester, date, time));
                if (worker.CancellationPending)
                {
                    throw new OperationCanceledException();
                }
                if (found == null)
                {
                    DateTime alternativDate = date;
                    int alternativTime = time;
                    int j = 0;
                    int i = 0;
                    while (found == null && j < 6)
                    {
                        if (worker.CancellationPending)
                        {
                            throw new OperationCanceledException();
                        }
                        if (++i > 5)
                        {
                            alternativTime = 9 + j;
                            j++;
                            i = 0;
                        }
                        if (alternativDate.DayOfWeek == DayOfWeek.Thursday)
                            alternativDate = alternativDate.AddDays(3);
                        else
                            alternativDate = alternativDate.AddDays(1);
                        found = testersList.Find(currentTester => isTesterAvailable(currentTester, alternativDate, alternativTime));
                    }
                    throw new AskedDateIsNotAvailableException(alternativDate, alternativTime, trainee, exitAddress, found, "There was no available tester in the asked date");
                }
                return new ExternalTester(found);
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        private bool isTesterAvailable(Tester tester, DateTime date, int time)
        {
            //weekend?
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Friday)
                return false;
            //works?
            if (tester.TableSchedule[(int)date.DayOfWeek, time - Configuration.TimeTestersBeginToWork] == false)
                return false;
            //is free?
            if (tester.Tests.ToDictionary(test => test.TestDate).ContainsKey(date) &&
                tester.Tests.ToDictionary(test => test.TestDate)[date].TestTime == time)
                return false;
            //aren't you too loaded
            if (tester.Tests.Count(test => isSameWeek(test.TestDate, date)) > tester.MaxTestsPerWeek)
                return false;
            return true;
        }
        private bool isSameWeek(DateTime date1, DateTime date2)
        {
            CultureInfo info = new CultureInfo("en-US");
            return
                info.Calendar.GetWeekOfYear(date1, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday) ==
                info.Calendar.GetWeekOfYear(date2, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday) &&
                date1.Year == date2.Year;
        }

        void IBL.addTester(Tester tester)
        {
            try
            {
                //is he too young?
                if (tester.CalculateAge() < Configuration.MinimumTesterAge)
                    throw new TooYoungeTesterException("tester too younge to add him.\n");
                //is he too old?
                if (tester.CalculateAge() > Configuration.MaximumTestersAge)
                    throw new TooOldTesterException("tester is too old to add him.\n");

                
                DO.Tester testerToAdd = tester.ToDOTester();
                dlObject.addTester(testerToAdd, tester.TableSchedule);
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        void IBL.addTrainee(Trainee trainee)
        {
            try
            {
                //is he too young?
                if (trainee.CalculateAge() < Configuration.MinimumTraineeAge)
                    throw new TooYoungeTraineeException("trainee is too younge to add him");
                DO.Trainee traineeToAdd = trainee.ToDOTrainee();
                dlObject.addTrainee(traineeToAdd);
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        #endregion

        #region Request Implementation
        Test IBL.getTest(int testNumber)
        {
            try
            {
                if (testNumber < Configuration.BeginningSerialTestNumber || testNumber > 99999999)
                    throw new InvalidTestNumberException("Invalid test number");

                DO.Test doTestSource = dlObject.getTest(testNumber);
                Test test = doTestSource.ToBOTest();
                addTesterDetails(doTestSource, test);
                addTraineeName(test);
                return test;
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        private void addTesterDetails(DO.Test doTestSorce, Test boTestToUpdate)
        {
            try
            {
                DO.Tester tester = dlObject.getTester(doTestSorce.TesterID);
                ExternalTester testerDetails = new ExternalTester(tester.ToBOTester());
                boTestToUpdate.Tester = testerDetails;
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        private void addTraineeName(Test boTestToUpdate)
        {
            try
            {
                DO.Trainee trainee = dlObject.getTrainee(boTestToUpdate.TraineeID);
                boTestToUpdate.TraineeName = trainee.FirstName + " " + trainee.LastName;
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        Tester IBL.getTester(int id)
        {
            try
            {
                Tester tester = dlObject.getTester(id).ToBOTester();
                addTesterSchedule(tester);
                addListOfTests(tester);
                return tester;
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        private void addTesterSchedule(Tester tester)
        {
            try
            {
                tester.TableSchedule = dlObject.getSchedule(tester.ID);
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        private void addListOfTests(Tester tester)
        {
            try
            {
                List<DO.Test> testList = dlObject.getCertainTestsList(test => test.TesterID == tester.ID);
                tester.Tests = testList.Select(test => new TesterTest(test.ToBOTest())).ToList();
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        Trainee IBL.getTrainee(int id)
        {
            try
            {
                Trainee trainee = dlObject.getTrainee(id).ToBOTrainee();
                addListOfTests(trainee);
                return trainee;
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        private void addListOfTests(Trainee trainee)
        {
            try
            {
                List<DO.Test> testList = dlObject.getCertainTestsList(test => test.TraineeID == trainee.ID);
                trainee.Tests = testList.Select(test => new TraineeTest(test.ToBOTest(), new ExternalTrainee(trainee))).ToList();
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        #endregion

        #region Update Implementation

        void IBL.updateTestAsAdmin(int testNumber, Test test)
        {
            try
            {
                if (testNumber < Configuration.BeginningSerialTestNumber || testNumber > 99999999)
                    throw new InvalidTestNumberException("Invalid test number.\n");

                //to do: make sure the tester inserted all the grades fields in his feedback
                DO.Test originalTest = dlObject.getTest(testNumber);
                //to do: update the coming test fields - date, tester, exitAddress ect.
                //is the test over?
                if (new DateTime().SystemNow() < originalTest.TestDate.AddHours(originalTest.TestTime).AddMinutes(40))
                    throw new FeedbackBeforeTestEndsException("You cannot give feedback before the test ends!\n");
                //is the test done and got the grades already?
                if (originalTest.TestAlreadyDoneAndSealed)
                    throw new TestHasBeenDoneAndSealedException("You cannot change details of a test after it has already got grades.\n");
                //does the feedback make sense?
                if (!gradesMakeSense(test))
                    throw new FeedbackMakesNoSenseException("Test feedback details do not make sense.\n");
                test.TestAlreadyDoneAndSealed = true;
                dlObject.updateTest(testNumber, test.ToDOTest());
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        void IBL.updateTest(int testNumber, TesterTest test, int testerID)
        {
            try
            {
                if (testNumber < Configuration.BeginningSerialTestNumber || testNumber > 99999999)
                    throw new InvalidTestNumberException("Invalid test number.\n");

                //to do: make sure the tester inserted all the grades fields in his feedback
                DO.Test originalTest = dlObject.getTest(testNumber);
                //to do: update the coming test fields - date, tester, exitAddress ect.
                //is the test over?
                if (new DateTime().SystemNow() < originalTest.TestDate.AddHours(originalTest.TestTime).AddMinutes(40))
                    throw new FeedbackBeforeTestEndsException("You cannot give feedback before the test ends!\n");
                //is the test done and got the grades already?
                if (originalTest.TestAlreadyDoneAndSealed)
                    throw new TestHasBeenDoneAndSealedException("You cannot change details of a test after it has already got grades.\n");
                //does the feedback make sense?
                if (!gradesMakeSense(test))
                    throw new FeedbackMakesNoSenseException("Test feedback details do not make sense.\n");
                test.TestAlreadyDoneAndSealed = true;
                dlObject.updateTest(testNumber, test.ToDOTest(testerID));
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        private bool gradesMakeSense(Test updatedTest)
        {
            if (updatedTest.PositiveFeedbackGradesPercentage() < Configuration.MinimumPercentsOfPositiveGrades
                && updatedTest.TestScore == true)
                return false;
            return true;
        }

        private bool gradesMakeSense(TesterTest updatedTest)
        {
            if (updatedTest.PositiveFeedbackGradesPercentage() < Configuration.MinimumPercentsOfPositiveGrades
                && updatedTest.TestScore == true)
                return false;
            return true;
        }

        void IBL.updateTester(int id, Tester updatedTester)
        {
            try
            {
                dlObject.updateTester(id, updatedTester.ToDOTester(), updatedTester.TableSchedule);
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }

        }

        void IBL.updateTrainee(int id, Trainee updatedTrainee)
        {
            try
            {
                DO.Trainee originalTrainee = dlObject.getTrainee(id);
                dlObject.updateTrainee(id, updatedTrainee.ToDOTrainee());
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        #endregion

        #region Delete Implementation 
        void IBL.removeTest(int testNumber)
        {
            try
            {
                if (testNumber < Configuration.BeginningSerialTestNumber || testNumber > 99999999)
                throw new InvalidTestNumberException("Invalid test number");
                DO.Test DOTest = dlObject.getTest(testNumber);
                DateTime testDateAndTime = DOTest.TestDate.AddHours(DOTest.TestTime); 
                //has the test done already?
                if (testDateAndTime <= new DateTime().SystemNow())
                    throw new TestCannotBeDeletedException("the test has already done and cannot be deleted");
                //are there enough days to the date to abort the test?
                if (testDateAndTime.AddDays(-Configuration.DaysBeforeTestATestCanBeDeleted) <= new DateTime().SystemNow())
                    throw new TestCannotBeDeletedException("You may not abort a test less then " + Configuration.DaysBeforeTestATestCanBeDeleted + " days before its time.");
                dlObject.removeTest(testNumber);
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        void IBL.removeTester(int id)
        {
            try
            {
                dlObject.removeTester(id);
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        void IBL.removeTrainee(int id)
        {
            try
            {
                dlObject.removeTrainee(id);
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }
        #endregion

        Dictionary<string, object> IBL.getConfig()
        {
            try
            {
                return dlObject.getConfig();
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        void IBL.setConfig(string parm, object value)
        {
            try
            {
                dlObject.setConfig(parm, value);
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        string IBL.getTypeOfUser(int id)
        {
            try
            {
                var Tester = dlObject.getCertainTestersList(tester => tester.ID == id);
                if (Tester.Any())
                    return "Tester";
                var Trainee = dlObject.getCertainTraineesList(trainee => trainee.ID == id);
                if (Trainee.Any())
                    return "Trainee";
                throw new NoSuchIdException("The ID Number is not registered to the system, you can register now.\n");
            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
        }

        //A function that initializes the schedule, the function accepts the current tester and its schedule.
        void IBL.setTesterSchedule(Tester tester, ListSchedule listSchedule)
        {
            int j = 0;
            //Build a matrix of working hours.
            bool[,] tableSchedule = new bool[5, 6];
            //Go over each day in the ScheduleAsList 
            foreach (var item in listSchedule.ScheduleAsList)
            {
                //They build a boolean array and put the working hours of the current tester
                bool[] hoursInDay = new bool[] { item.Nine, item.Ten, item.Eleven, item.Twelve, item.Thirteen, item.Fourteen };
                //Converting the working hours from the array into the matrix
                for (int i = 0; i < 6; i++)
                {
                    tableSchedule[j, i] = hoursInDay[i];
                }
                //Move to the second row in the matrix
                j++;
            }
            //We initialize the matrix of the taster from the matrix we built
            tester.TableSchedule = tableSchedule;
        }
        // A function that returns the shedules of the current tester
        ListSchedule IBL.getTesterSchedule(Tester tester)
        {
            ListSchedule listschedule = new ListSchedule();
            //Convert from matrix to list Each object in the list contains the working hours of a particular day.
            listschedule.ScheduleAsList[0] = (new ListSchedule.coulmonInputs("Sunday", tester.TableSchedule[0, 0], tester.TableSchedule[0, 1], tester.TableSchedule[0, 2], tester.TableSchedule[0, 3], tester.TableSchedule[0, 4], tester.TableSchedule[0, 5]));
            listschedule.ScheduleAsList[1] = (new ListSchedule.coulmonInputs("Monday", tester.TableSchedule[1, 0], tester.TableSchedule[1, 1], tester.TableSchedule[1, 2], tester.TableSchedule[1, 3], tester.TableSchedule[1, 4], tester.TableSchedule[1, 5]));
            listschedule.ScheduleAsList[2] = (new ListSchedule.coulmonInputs("Tuesday", tester.TableSchedule[2, 0], tester.TableSchedule[2, 1], tester.TableSchedule[2, 2], tester.TableSchedule[2, 3], tester.TableSchedule[2, 4], tester.TableSchedule[2, 5]));
            listschedule.ScheduleAsList[3] = (new ListSchedule.coulmonInputs("Wednesday", tester.TableSchedule[3, 0], tester.TableSchedule[3, 1], tester.TableSchedule[3, 2], tester.TableSchedule[3, 3], tester.TableSchedule[3, 4], tester.TableSchedule[3, 5]));
            listschedule.ScheduleAsList[4] = (new ListSchedule.coulmonInputs("Thursday", tester.TableSchedule[4, 0], tester.TableSchedule[4, 1], tester.TableSchedule[4, 2], tester.TableSchedule[4, 3], tester.TableSchedule[4, 4], tester.TableSchedule[4, 5]));

            return listschedule;
        }

        List<Tester> IBL.getTestersByVehicleType(bool sort)
        {
            try
            {
                List<Tester> testersList = (from DO.Tester DOtester in dlObject.getTesterList()
                                            select DOtester.ToBOTester()).ToList();
                testersList.GroupBy(tester => tester.CarType);

                if (sort)
                {
                    testersList.OrderBy(tester => tester.LastName).ThenBy(tester => tester.FirstName).GroupBy(tester => tester.CarType);
                }
                return testersList;

            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
            
        }

        List<Trainee> IBL.getTraineesBySchool(bool sort)
        {
            try
            {
                List<Trainee> traineesList = (from DO.Trainee DOtrainee in dlObject.getTraineeList()
                                              select DOtrainee.ToBOTrainee()).ToList();
                traineesList.GroupBy(trainee => trainee.CarType);

                if (sort)
                {
                    traineesList.OrderBy(trainee => trainee.LastName).ThenBy(trainee => trainee.FirstName).GroupBy(trainee => trainee.CarType);
                }
                return traineesList;

            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
            
        }

        List<Trainee> IBL.getTraineesByTeacher(bool sort)
        {
            try
            {
                List<Trainee> traineesList = (from DO.Trainee DOtrainee in dlObject.getTraineeList()
                                              select DOtrainee.ToBOTrainee()).ToList();
                traineesList.GroupBy(trainee => trainee.TeacherName);

                if (sort)
                {
                    traineesList.OrderBy(trainee => trainee.LastName).ThenBy(trainee => trainee.FirstName).GroupBy(trainee => trainee.TeacherName);
                }
                return traineesList;

            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
            
        }

        List<Trainee> IBL.getTraineesByAmountOfTests( bool sort)
        {
            try
            {
                List<Trainee> traineesList = (from DO.Trainee DOtrainee in dlObject.getTraineeList()
                                              select DOtrainee.ToBOTrainee()).ToList();
                traineesList.GroupBy(trainee => trainee.TestCount());

                if (sort)
                {
                    traineesList.OrderBy(trainee => trainee.LastName).ThenBy(trainee => trainee.FirstName).GroupBy(trainee => trainee.TestCount());
                }
                return traineesList;

            }
            catch (Exception exception)
            {
                throw exception.ToBOException();
            }
            
        }

        void IBL.setSystemTime(DateTime date)
        {
            if (date < new DateTime().SystemNow())
                throw new SystemTimeSetToPastException("Cannot set teh system time to a past time");
            SystemTimeFromRealTimeSpan = date - new DateTime().SystemNow();
        }

        DateTime IBL.getSystemTime()
        {
            return new DateTime().SystemNow();
        }

        List<Test> IBL.getTests()
        {
            List<Test> testsList = dlObject.getTestList().Select(test => test.ToBOTest()).ToList();

            return testsList;
        }

        List<TesterTest> IBL.getTestswaitingForfeedback(int testerId)
        {
            List<TesterTest> waitingTests = (from doTest in dlObject.getCertainTestsList(test => test.TesterID == testerId)
                                      where (doTest.TestDate < new DateTime().SystemNow() 
                                      && doTest.TestAlreadyDoneAndSealed == false)
                                      select new TesterTest( doTest.ToBOTest())).ToList();
            return waitingTests;
                                      
        }

        void IBL.addTestWithFoundTester(Trainee trainee, DateTime date, int time, Address exitAddress, Tester foundTester)
        {
            ExternalTester tester = new ExternalTester(foundTester);
            Test test = new Test(
                 0,
                 tester,
                 trainee.ID,
                 trainee.FirstName + " " + trainee.LastName,
                 date,
                 time,
                 exitAddress);
            DO.Test testToAdd = test.ToDOTest();
            int fixedTestNumber = dlObject.addTest(testToAdd);
            trainee.Tests.Add(
                new TraineeTest(dlObject.getTest(fixedTestNumber).ToBOTest(),
                new ExternalTrainee(trainee)));
        }

        void IBL.getAllCloseTesters(Address address, double maxDistance, BackgroundWorker worker)
        {
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += calculateDistanceWorker_DoWork;
            worker.RunWorkerAsync(new List<object>() { address, maxDistance });
            
        }

        private void calculateDistanceWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                List<object> parms = e.Argument as List<object>;
                Address address = (Address)parms[0];
                double maxDistance = (double)parms[1];
                List<Tester> closeTesters = dlObject.getCertainTestersList
                    (tester => distance(tester.Address.ToBOAddress(), address, worker) <= maxDistance).
                    Select(x => x.ToBOTester()).ToList();
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                foreach (Tester BOtester in closeTesters)
                {
                    addListOfTests(BOtester);
                    addTesterSchedule(BOtester);
                }
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                e.Result = closeTesters;
            }
            catch (Exception ex)
            {
                e.Result = ex.ToBOException();
            }
        }

        List<Tester> IBL.getAllAvailibleTesters(DateTime date, int time)
        {
            try
            {
                List<Tester> testers = dlObject.getTesterList().Select(tester => tester.ToBOTester()).ToList();
                foreach(Tester tester in testers)
                {
                    addListOfTests(tester);
                    addTesterSchedule(tester);
                }
                List<Tester> availableTesters = (from Tester tester in testers
                                                 where isTesterAvailable(tester, date, time)
                                                 select tester).ToList();
                return availableTesters;
            }
            catch (Exception ex)
            {
                throw ex.ToBOException();
            }
        }

        List<Tester> IBL.getAllTooOldTesters()
        {
            try
            {
                List<Tester> oldOnes = dlObject.getTesterList().
                Select(tester => tester.ToBOTester()).
                Where(tester => tester.CalculateAge() > Configuration.MaximumTestersAge).
                ToList();
                return oldOnes;
            }
            catch (Exception ex)
            {

                throw ex.ToBOException();
            }
            
        }

        int IBL.getTraineesNumberOfTests(Trainee trainee)
        {
            return trainee.TestCount();
        }

        bool IBL.isTraineeCertifiedToDrive(Trainee trainee)
        {
            try
            {
                TraineeTest successfulTest = trainee.Tests.Find(test => test.TestScore == true);
                if (successfulTest == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {

                throw ex.ToBOException();
            }
            
        }

        List<Trainee> IBL.getAllTraineesAllowedForTest()
        {
            try
            {
                var trainees = dlObject.getTraineeList().Select(trainee => trainee.ToBOTrainee()).ToList();
                foreach (var item in trainees)
                {
                    addListOfTests(item);
                }
                List<Trainee> allowedTrainees = trainees.Where(trainee => isTraineeAllowed(trainee)).ToList();
                return allowedTrainees;

            }
            catch (Exception ex)
            {

                throw ex.ToBOException();
            }
        }
        private bool isTraineeAllowed(Trainee trainee)
        {
            try
            {
                //learned enough lessons?
                if (trainee.NumberOfLessons < Configuration.MinimumNumberOfLessons)
                    return false;
                //it is not his first test! hmmm...
                if (trainee.Tests.Any())
                {
                    //is he waiting for a test?
                    var inCompleteTestDate = from test in trainee.Tests
                                             where test.TestAlreadyDoneAndSealed == false
                                             select test.TestDate;
                    if (inCompleteTestDate.Any())
                        return false;
                    //has he passed a test already?
                    var successfulTest = from test in trainee.Tests
                                         where test.TestScore == true
                                         select test.TestNumber;
                    if (successfulTest.Any())
                        return false;
                    //enough time between the last test and the asked date?
                    DateTime lastTestDate = trainee.Tests.Max(test => test.TestDate);
                    if ((new DateTime().SystemNow() - lastTestDate).Days < Configuration.DaysIntervalBetweenTests)
                        return false;
                }
                //in case all the checks were positive
                return true;
            }
            catch (Exception exception)
            {

                throw exception.ToBOException();
            }
        }

        List<Test> IBL.getCertainTests(Predicate<Test> pred)
        {
            try
            {
                var tests = dlObject.getTestList().Select(test => test.ToBOTest());
                List<Test> matchingTrainees = tests.Where(test => pred(test)).ToList();
                return matchingTrainees;
            }
            catch (Exception ex)
            {
                throw ex.ToBOException();
            }
        }

        List<Test> IBL.getAllTestsInaDay(DateTime date)
        {
            try
            {
                List<Test> tests = dlObject.getCertainTestsList(test => test.TestDate.Date == date.Date).
                    Select(test => test.ToBOTest()).ToList();
                return tests;
            }
            catch (Exception ex)
            {
                throw ex.ToBOException();
            }
        }

        List<Test> IBL.getAllTestsInaMonth(DateTime date)
        {
            try
            {
                List<Test> tests = dlObject.getCertainTestsList(test => test.TestDate.Month == date.Month).
                    Select(test => test.ToBOTest()).ToList();
                return tests;
            }
            catch (Exception ex)
            {
                throw ex.ToBOException();
            }
        }

        List<string> IBL.getCitiesOfTrainees()
        {
            var traineesCities = dlObject.getTraineeList().Select(trainee => trainee.Address.City);
            return traineesCities.Distinct().ToList();
        }

        List<string> IBL.getCitiesOfTesters()
        {
            var testersCities = dlObject.getTesterList().Select(tester => tester.Address.City);
            return testersCities.Distinct().ToList();
        }

        List<Tester> IBL.getTesters()
        {
            var testers = dlObject.getTesterList().Select(tester => tester.ToBOTester()).ToList();
            foreach (Tester tester in testers)
            {
                addListOfTests(tester);
                addTesterSchedule(tester);
            }
            return testers;
        }
        List<Trainee> IBL.getTrainees()
        {
            var trainees = dlObject.getTraineeList().Select(trainee => trainee.ToBOTrainee()).ToList();
            foreach (Trainee trainee in trainees)
            {
                addListOfTests(trainee);
            }
            return trainees;
        }
    }
}
