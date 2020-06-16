using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DL
{
    public interface IDL
    {
        #region Tester Functions
        void addTester(Tester tester, bool[,] schedule);
        void removeTester(int id);
        void updateTester(int id, Tester tester, bool[,] schedule);
        Tester getTester(int id);
        #endregion
        #region Trainee Functions
        void addTrainee(Trainee tester);
        void removeTrainee(int id);
        void updateTrainee(int id, Trainee tester);
        Trainee getTrainee(int id);
        #endregion
        #region Test Functions
        void removeTest(int testNumber);
        int addTest(Test testToAdd);
        void updateTest(int testNumber, Test test);
        Test getTest(int testNumber);
        #endregion
        List<Tester> getTesterList();
        List<Trainee> getTraineeList();
        List<Test> getTestList();
        List<Tester> getCertainTestersList(Predicate<Tester> pred);
        List<Trainee> getCertainTraineesList(Predicate<Trainee> pred);
        List<Test> getCertainTestsList(Predicate<Test> pred);
        Dictionary<string, object> getConfig();
        void setConfig(string parm, object value);
        bool[,] getSchedule(int testerID);
    }
}

