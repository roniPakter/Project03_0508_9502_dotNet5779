using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Test
    {
        public int TestNumber { get; set; }
        public ExternalTester Tester { get; set; }
        public int TraineeID { get; set; }
        //full name
        public string TraineeName { get; set; }
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

        public Test(int testNumber, ExternalTester tester, int traineeID, string traineeName, DateTime testDate, int testTime, Address exitAddress,
           bool testAlreadyDone = false, bool distanceKeeping = false, bool reverseParking = false, bool lookingAtMirrors = false, bool signalsUsage = false, bool priorityGiving = false, bool speedKeeping = false, bool testScore = false, string testersNote = null)
        {
            TestNumber = testNumber;
            Tester = tester;
            TraineeID = traineeID;
            TraineeName = traineeName;
            TestDate=testDate;
            TestTime = testTime;
            ExitAddress = exitAddress;

            TestAlreadyDoneAndSealed = testAlreadyDone;
            DistanceKeeping = distanceKeeping;
            ReverseParking = reverseParking;
            LookingAtMirrors = lookingAtMirrors;
            SignalsUsage = signalsUsage;
            PriorityGiving = priorityGiving;
            SpeedKeeping = speedKeeping;
            TestScore = testScore;
            TestersNote = testersNote;
        }

        public Test(Test boTestSource)
        {
            TestNumber = boTestSource.TestNumber;
            Tester = boTestSource.Tester;
            TraineeID = boTestSource.TraineeID;
            TraineeName = boTestSource.TraineeName;
            TestDate = boTestSource.TestDate;
            TestTime = boTestSource.TestTime;
            ExitAddress = boTestSource.ExitAddress;

            TestAlreadyDoneAndSealed = boTestSource.TestAlreadyDoneAndSealed;
            DistanceKeeping = boTestSource.DistanceKeeping;
            ReverseParking = boTestSource.ReverseParking;
            LookingAtMirrors = boTestSource.LookingAtMirrors;
            SignalsUsage = boTestSource.SignalsUsage;
            PriorityGiving = boTestSource.PriorityGiving;
            SpeedKeeping = boTestSource.SpeedKeeping;
            TestScore = boTestSource.TestScore;
            TestersNote = boTestSource.TestersNote;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        public override string ToString()
        {
            string str;
            str = string.Format("Test Number:{0}\nDate:{1}\nOn: {2} o'clock\nExit Address: {3}\n", TestNumber, TestDate, TestTime, ExitAddress);
            if (TestAlreadyDoneAndSealed)
            {
                if (TestScore)
                    str += "The student passed the test successfully.\n" + "Tester's Note: " + TestersNote + "\n";
                else str += "The student failed the test.\n" + "Tester's Note: " + TestersNote + "\n";
            }
            return str;
        }
    }
}
