using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DO;


//
namespace DL
{
    internal class Dal_XML_imp : IDL
    {
        private static Dal_XML_imp instance { get; set; } = null;
        private XElement TraineeRoot { get; set; }
        private XElement TesterRoot { get; set; }
        private XElement TestRoot { get; set; }
        private XElement ConfigRoot { get; set; }
        private XElement ScheduleRoot { get; set; }
        private string TraineePath { get; set; } = @"TraineeXml.xml";
        private string TesterPath { get; set; } = @"TesterXml.xml";
        private string TestPath { get; set; } = @"TestXml.xml";
        private string ConfigPath { get; set; } = @"ConfigXml.xml";
        private string SchedulePath { get; set; } = @"ScheduleXml.xml";

        private Dal_XML_imp()
        {
            if (!File.Exists(TraineePath))
                CreateFiles("trainees");
            else
                LoadData("trainees");
            if (!File.Exists(TesterPath))
                CreateFiles("testers");
            else
                LoadData("testers");
            if (!File.Exists(TestPath))
                CreateFiles("tests");
            else
                LoadData("tests");
            if (!File.Exists(ConfigPath))
                CreateFiles("configuration");
            else
                LoadData("configuration");
            if (!File.Exists(SchedulePath))
                CreateFiles("schedules");
            else
                LoadData("schedules");
        }

        internal static Dal_XML_imp getInstance()
        {
            if (instance == null)
                instance = new Dal_XML_imp();
            return instance;

        }
        private void CreateFiles(string identifier)
        {
            switch (identifier)
            {
                case "trainees":
                    TraineeRoot = new XElement("trainees");
                    TraineeRoot.Save(TraineePath);
                    break;
                case "testers":
                    TesterRoot = new XElement("testers");
                    TesterRoot.Save(TesterPath);
                    break;
                case "tests":
                    TestRoot = new XElement("tests");
                    TestRoot.Save(TestPath);
                    break;
                case "configuration":
                    ConfigRoot = new XElement("Configuration",
                        configElementBuilder("MinimumTesterAge", "24", true, true),
                        configElementBuilder("MinimumTraineeAge", "17", true, true),
                        configElementBuilder("DaysIntervalBetweenTests", "14", true, true),
                        configElementBuilder("MaximumTestersAge", "65", true, true),
                        configElementBuilder("MinimumNumberOfLessons", "28", true, true),
                        configElementBuilder("BeginningSerialTestNumber", "10000000", true, true),
                        configElementBuilder("MinimumPercentsOfPositiveGrades", "50", true, true),
                        configElementBuilder("TimeTestersBeginToWork", "9", true, true),
                        configElementBuilder("DaysBeforeTestATestCanBeDeleted", "2", true, true));
                    ConfigRoot.Save(ConfigPath);
                    break;
                case "schedules":
                    ScheduleRoot = new XElement("Schedules");
                    ScheduleRoot.Save(SchedulePath);
                    break;
            }
        }

        private XElement configElementBuilder(string configKey, string configValue, bool readable, bool writeable)
        {
            XElement key = new XElement("Key", configKey);
            XElement value = new XElement("Value", configValue);
            XElement isReadable = new XElement("IsReadable", readable);
            XElement isWriteable = new XElement("IsWriteable", writeable);
            return new XElement("Config", key, value, isReadable, isWriteable);
        }

        private void LoadData(string identifier)
        {
            try
            {
                switch (identifier)
                {
                    case "trainees":
                        TraineeRoot = XElement.Load(TraineePath);
                        break;
                    case "testers":
                        TesterRoot = XElement.Load(TesterPath);
                        break;
                    case "tests":
                        TestRoot = XElement.Load(TestPath);
                        break;
                    case "configuration":
                        ConfigRoot = XElement.Load(ConfigPath);
                        break;
                    case "schedules":
                        ScheduleRoot = XElement.Load(SchedulePath);
                        break;

                }
            }
            catch
            {
                throw new Exception("File uplode problem");
            }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        int IDL.addTest(Test testToAdd)
        {
            //to do: make a running code for tests
            int testCode = int.Parse(ConfigRoot.Element("BeginningSerialTestNumber").Element("Value").Value);
            int testNumber = (from test in TestRoot.Elements()
                              where (int.Parse(test.Element("TestNumber").Value) == testToAdd.TestNumber)
                              select (int.Parse(test.Element("TestNumber").Value))).FirstOrDefault();
            if (testNumber != 0)
                throw new TestNumberAlreadyExistsException(testNumber);

            testToAdd.TestNumber = testCode;
            TestRoot.Add(testToAdd.ToXmlTest());
            TestRoot.Save(TestPath);
            ConfigRoot.Element("BeginningSerialTestNumber").Element("Value").Value = (testCode + 1).ToString();
            ConfigRoot.Save(ConfigPath);
            return testCode;
        }

        void IDL.addTester(Tester tester, bool[,] schedule)
        {
            int id = (from item in TesterRoot.Elements()
                      where (int.Parse(item.Element("ID").Value) == tester.ID)
                      select (int.Parse(item.Element("ID").Value))).FirstOrDefault();
            if (id != 0)
                throw new TestersIdAlreadyExistsException(tester.ID, tester.FirstName + " " + tester.LastName);
            
            TesterRoot.Add(tester.ToXmlTester());
            TesterRoot.Save(TesterPath);

            XElement testersId = new XElement("ID", tester.ID.ToString());
            ScheduleRoot.Add(new XElement("TesterSchedule", testersId, schedule.ToXmlSchedule()));
            ScheduleRoot.Save(SchedulePath);
        }

        void IDL.addTrainee(Trainee trainee)
        {
            int id = (from item in TraineeRoot.Elements()
                      where (int.Parse(item.Element("ID").Value) == trainee.ID)
                      select (int.Parse(item.Element("ID").Value))).FirstOrDefault();
            if (id != 0)
                throw new TraineesIdAlreadyExistsException(trainee.ID, trainee.FirstName + " " + trainee.LastName);

            TraineeRoot.Add(trainee.ToXmlTrainee());
            TraineeRoot.Save(TraineePath);

        }

        List<Tester> IDL.getCertainTestersList(Predicate<Tester> pred)
        {
            List<Tester> testerList = (from XElement testerElement in TesterRoot.Elements()
                                       select testerElement.XmlToDOTester()).ToList();
            List<Tester> certainTestersList = (from Tester tester in testerList
                                               where pred(tester)
                                               select tester).ToList();
            return certainTestersList;
        }

        List<Test> IDL.getCertainTestsList(Predicate<Test> pred)
        {
            List<Test> testList = (from XElement testElement in TestRoot.Elements()
                                   select testElement.XmlToDOTest()).ToList();
            List<Test> certainTestsList = (from Test test in testList
                                           where pred(test)
                                           select test).ToList();
            return certainTestsList;
        }

        List<Trainee> IDL.getCertainTraineesList(Predicate<Trainee> pred)
        {
            List<Trainee> traineeList = (from XElement traineeElement in TraineeRoot.Elements()
                                         select traineeElement.XmlToDOTrainee()).ToList();
            List<Trainee> certainTraineesList = (from Trainee trainee in traineeList
                                                 where pred(trainee)
                                                 select trainee).ToList();
            return certainTraineesList;
        }

        Dictionary<string, object> IDL.getConfig()
        {
            var config = (from XElement configElement in ConfigRoot.Elements()
                          where bool.Parse(configElement.Element("IsReadable").Value)
                          select configElement);
            Dictionary<string, object> configDic = config.
                                                 ToDictionary
                                                (configItem => configItem.Element("Key").Value,
                                                configItem => (object)configItem.Element("Value").Value);
            return configDic;
        }

        bool[,] IDL.getSchedule(int testerID)
        {
            //if (!ScheduleRoot.Element("schedules").HasElements)
            //    throw new Exception("No Schedule in the dataSource");

            XElement schedule = 
                ScheduleRoot.Elements()
                .Where(item => item.Element("ID").Value == testerID.ToString())
                .FirstOrDefault();
            if (schedule == null)
                throw new ScheduleWasNotInsertedException("the tester's schedule was not inserted yet");

            return schedule.Element("Schedule").XmlToDOSchedule();
        }

        Test IDL.getTest(int testNumber)
        {
            //if (!TraineeRoot.Element("tests").HasElements)
            //    throw new TestersDataSourceIsEmptyException("No Test in the dataSource");
            XElement foundTest = (from XElement testElement in TestRoot.Elements()
                                  where testElement.Element("TestNumber").Value.Equals(testNumber.ToString())
                                  select testElement).FirstOrDefault();
            if (foundTest == null)
                throw new TestersIdNotFoundException(testNumber);
            return foundTest.XmlToDOTest();
        }

        Tester IDL.getTester(int id)
        {
            XElement foundTester = (from XElement testerElement in TesterRoot.Elements()
                                    where testerElement.Element("ID").Value.Equals(id.ToString())
                                    select testerElement).FirstOrDefault();
            if (foundTester == null)
                throw new TestersIdNotFoundException(id);
            return foundTester.XmlToDOTester();
        }

        List<Tester> IDL.getTesterList()
        {
            List<Tester> testersList = (from XElement testerElement in TesterRoot.Elements()
                                        select testerElement.XmlToDOTester()).ToList();
            return testersList;
        }

        List<Test> IDL.getTestList()
        {
            List<Test> testList = (from XElement testElement in TestRoot.Elements()
                                   select testElement.XmlToDOTest()).ToList();
            return testList;
        }

        Trainee IDL.getTrainee(int id)
        {
            //if (!TraineeRoot.Element("trainee").HasElements)
            //    throw new Exception("No Trainees in the dataSource");
            XElement foundTrainee = (from XElement traineeElement in TraineeRoot.Elements()
                                     where traineeElement.Element("ID").Value.Equals(id.ToString())
                                     select traineeElement).FirstOrDefault();
            if (foundTrainee == null)
                throw new TraineesIdNotFoundException(id);
            return foundTrainee.XmlToDOTrainee();

        }

        List<Trainee> IDL.getTraineeList()
        {
            List<Trainee> traineeList = (from XElement traineeElement in TraineeRoot.Elements()
                                         select traineeElement.XmlToDOTrainee()).ToList();
            return traineeList;
        }

        void IDL.removeTest(int testNumber)
        {
            XElement testElement = (from test in TestRoot.Elements()
                                    where int.Parse(test.Element("TestNumber").Value) == testNumber
                                    select test).FirstOrDefault();
            if (testElement == null)
                throw new TestNumberNotFoundException("thrown from: DL.Dal_XML_imp.removeTest");
            testElement.Remove();
            TestRoot.Save(TestPath);
        }

        void IDL.removeTester(int id)
        {
            XElement testerElement = (from tester in TesterRoot.Elements()
                                      where int.Parse(tester.Element("ID").Value) == id
                                      select tester).FirstOrDefault();
            if (testerElement == null)
                throw new TestNumberNotFoundException("thrown from: DL.Dal_XML_imp.removeTester");
            testerElement.Remove();
            TesterRoot.Save(TesterPath);

        }

        void IDL.removeTrainee(int id)
        {
            XElement traineeElement = (from trainee in TraineeRoot.Elements()
                                       where int.Parse(trainee.Element("ID").Value) == id
                                       select trainee).FirstOrDefault();
            if (traineeElement == null)
                throw new TraineesIdNotFoundException(id);
            traineeElement.Remove();
            TraineeRoot.Save(TraineePath);
        }

        void IDL.setConfig(string parm, object value)
        {
            XElement configElement = ConfigRoot.Elements(parm).FirstOrDefault();
            if (configElement == null)
                throw new ConfigurationValueNotExistException("the inserted value does not exist in the configuration");
            if (bool.Parse(configElement.Element("IsWriteable").Value))
                configElement.Element("Value").Value = value.ToString();
            else
                throw new ConfigurationValueNotWriteableException(parm);
        }

        void IDL.updateTest(int testNumber, Test updatedTest)
        {
            XElement testElement = (from testItem in TestRoot.Elements()
                                    where int.Parse(testItem.Element("TestNumber").Value) == testNumber
                                    select testItem).FirstOrDefault();
            if (testElement == null)
                throw new TestNumberNotFoundException(testNumber);
            testElement.Remove();
            TestRoot.Add(updatedTest.ToXmlTest());
            TestRoot.Save(TestPath);
        }

        void IDL.updateTester(int id, Tester updatedTester, bool[,] updatedSchedule)
        {
            XElement testerElement = (from item in TesterRoot.Elements()
                                      where int.Parse(item.Element("ID").Value) == id
                                      select item).FirstOrDefault();
            if (testerElement == null)
                throw new TestersIdNotFoundException(id);
            testerElement.Remove();
            TesterRoot.Add(updatedTester.ToXmlTester());
            TesterRoot.Save(TesterPath);

            XElement scheduleXelement =
                ScheduleRoot.Elements()
                .Where(item => item.Element("ID").Value == id.ToString())
                .FirstOrDefault();
            if (scheduleXelement != null)
                scheduleXelement.Remove();

            XElement testersId = new XElement("ID", id.ToString());
            ScheduleRoot.Add(new XElement("TesterSchedule", testersId, updatedSchedule.ToXmlSchedule()));
            ScheduleRoot.Save(SchedulePath);
        }

        void IDL.updateTrainee(int id, Trainee updatedTrainee)
        {
            XElement traineeElement = (from item in TraineeRoot.Elements()
                                       where int.Parse(item.Element("ID").Value) == id
                                       select item).FirstOrDefault();
            if (traineeElement == null)
                throw new TraineesIdNotFoundException(id);
            traineeElement.Remove();
            TraineeRoot.Add(updatedTrainee.ToXmlTrainee());
            TraineeRoot.Save(TraineePath);
        }
    }
}
