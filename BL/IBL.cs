using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.ComponentModel;
using System.Collections;

namespace BL
{
    public interface IBL
    {
        #region Tester Functions
        void addTester(Tester tester);
        void removeTester(int id);
        void updateTester(int id, Tester tester);
        Tester getTester(int id);
        void setTesterSchedule(Tester tester, ListSchedule listSchedule);
        ListSchedule getTesterSchedule(Tester tester);
        void getAllCloseTesters(Address address, double maxDistance, BackgroundWorker worker);
        List<Tester> getAllAvailibleTesters(DateTime date, int time);
        List<Tester> getAllTooOldTesters();
        #endregion

        #region Trainee Functions
        void addTrainee(Trainee trainee);
        void removeTrainee(int id);
        void updateTrainee(int id, Trainee updatedTrainee);
        Trainee getTrainee(int id);
        List<string> getCitiesOfTrainees();
        int getTraineesNumberOfTests(Trainee trainee);
        bool isTraineeCertifiedToDrive(Trainee trainee);
        List<Trainee> getAllTraineesAllowedForTest();
        #endregion

        #region Test Functions
        void removeTest(int testNumber);
        List<string> getCitiesOfTesters();
        void addTest(BackgroundWorker addTestWorker, Trainee trainee, DateTime askedDate, int askedTime, Address exitAddress);
        void addTestWithFoundTester(Trainee trainee, DateTime alternativeDate, int alternativeTime, Address exitAddress, Tester foundTester);
        void updateTestAsAdmin(int testNumber, Test test);
        void updateTest(int testNumber, TesterTest test, int testerID);
        Test getTest(int testNumber);
        List<Test> getTests();
        List<TesterTest> getTestswaitingForfeedback(int TesterId);
        List<Test> getCertainTests(Predicate<Test> pred);
        List<Test> getAllTestsInaDay(DateTime date);
        List<Test> getAllTestsInaMonth(DateTime date);
        #endregion

        #region Grouped Lists Functions
        List<Tester> getTestersByVehicleType(bool sort = false);
        List<Trainee> getTraineesBySchool(bool sort = false);
        List<Trainee> getTraineesByTeacher(bool sort = false);
        List<Trainee> getTraineesByAmountOfTests(bool sort = false);
        #endregion
        Dictionary<string, object> getConfig();
        void setConfig(string parm, object value);
        string getTypeOfUser(int id);
        
        void setSystemTime(DateTime date);
        DateTime getSystemTime();
        List<Tester> getTesters();
        List<Trainee> getTrainees();
    }
}
