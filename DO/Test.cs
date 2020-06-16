using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class Test
    {
        public int TestNumber { get; set; }
        public int TesterID { get; set; }
        public int TraineeID { get; set; }
        public DateTime TestDate { get; set; }
        public int TestTime { get; set; }

        public Address ExitAddress { get; set; }

        public bool TestAlreadyDoneAndSealed { get; set; }
        public bool DistanceKeeping { get; set; }
        public bool ReverseParking { get; set; }
        public bool LookingAtMirrors { get; set; }
        public bool SignalsUsage { get; set; }
        public bool PriorityGiving { get; set; }
        public bool SpeedKeeping { get; set; }
        public bool TestScore { get; set; }
        public string TestersNote { get; set; }

        public Test(int testCode, int testerID, int studentID, DateTime testDate, int testTime, Address exitAddress, bool testAlreadyDone = false , bool distanceKeeping = false, bool reverseParking = false, bool lookingAtMirrors = false, bool signalsUsage = false, bool priorityGiving = false, bool speedKeeping = false, bool testScore = false, string testersNote = "")
        {
            TestNumber = testCode;
            TestAlreadyDoneAndSealed = testAlreadyDone;
            DistanceKeeping = distanceKeeping;
            ExitAddress = exitAddress;
            LookingAtMirrors = lookingAtMirrors;
            PriorityGiving = priorityGiving;
            ReverseParking = reverseParking;
            SignalsUsage = signalsUsage;
            SpeedKeeping = speedKeeping;
            TraineeID = studentID;
            TestDate = testDate;
            TesterID = testerID;
            TestersNote = testersNote;
            TestScore = testScore;
            TestTime = testTime;
   
        }

        [Obsolete]
        public Test(Test sourceTest)
        {
            TestAlreadyDoneAndSealed = sourceTest.TestAlreadyDoneAndSealed;
            TestNumber = sourceTest.TestNumber;
            DistanceKeeping = sourceTest.DistanceKeeping;
            ExitAddress = sourceTest.ExitAddress;
            LookingAtMirrors = sourceTest.LookingAtMirrors;
            PriorityGiving = sourceTest.PriorityGiving;
            ReverseParking = sourceTest.ReverseParking;
            SignalsUsage = sourceTest.SignalsUsage;
            SpeedKeeping = sourceTest.SpeedKeeping;
            TraineeID = sourceTest.TraineeID;
            TestDate = sourceTest.TestDate;
            TesterID = sourceTest.TesterID;
            TestersNote = sourceTest.TestersNote;
            TestScore = sourceTest.TestScore;
            TestTime = sourceTest.TestTime;
        }

        public override string ToString()
        {
            string str;
            str= string.Format("Test Number:{0}\nDate:{1}\nOn: {2} o'clock\nExit Address: {3}\n", TestNumber, TestDate, TestTime, ExitAddress);
            if (TestAlreadyDoneAndSealed)
            {
                if(TestScore)
                str += "The student passed the test successfully.\n" + "Tester's Note: " + TestersNote + "\n";
                else str += "The student failed the test.\n"  + "Tester's Note: " + TestersNote + "\n";
            }
            return str;
        }

    }
}