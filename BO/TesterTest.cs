using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TesterTest
    {
        public int TestNumber { get; private set; }
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
       

        public TesterTest() {}
        

        public TesterTest(Test boTestSource)
        {
            TestNumber = boTestSource.TestNumber;
            TraineeID = boTestSource.TraineeID;
            TestDate = boTestSource.TestDate;
            TestTime = boTestSource.TestTime;
            ExitAddress = boTestSource.ExitAddress;

            // grades
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

        public override string ToString()
        {
            string str;
            str = string.Format("Test Number:{0}\nDate:{1}\nOn: {2} o'clock\nExit Address: {3}\n", TestNumber, TestDate, TestTime, ExitAddress);
            if (TestAlreadyDoneAndSealed)
            {
                if (TestScore) str = "The student passed the test successfully.\n";
                else str = "The student failed the test.\n";
            }
            return str;
        }
    }
}
