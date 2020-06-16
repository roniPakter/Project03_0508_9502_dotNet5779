using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TraineeTest
    {
        public int TestNumber { get; set; }
        public ExternalTrainee Trainee { get; set; }
        public DateTime TestDate { get; set; }
        public int TestTime { get; set; }

        public Address ExitAddress { get; set; }
        //grades:
        public bool TestAlreadyDoneAndSealed { get; set; }
        public bool DistanceKeeping { get; set; }
        public bool ReverseParking { get; set; }
        public bool LookingAtMirrors { get; set; }
        public bool SignalsUsage { get; set; }
        public bool PriorityGiving { get; set; }
        public bool SpeedKeeping { get; set; }
        public bool TestScore { get; set; }
        public string TestersNote { get; set; }

        public TraineeTest() { }
        
        public TraineeTest(Test boTest, ExternalTrainee trainee)
        {
            TestNumber = boTest.TestNumber;
            Trainee = trainee;
            TestDate = boTest.TestDate;
            TestTime = boTest.TestTime;
            ExitAddress = boTest.ExitAddress;

            TestAlreadyDoneAndSealed = boTest.TestAlreadyDoneAndSealed;
            DistanceKeeping = boTest.DistanceKeeping;
            ReverseParking = boTest.ReverseParking;
            LookingAtMirrors = boTest.LookingAtMirrors;
            SignalsUsage = boTest.SignalsUsage;
            PriorityGiving = boTest.PriorityGiving;
            SpeedKeeping = boTest.SpeedKeeping;
            TestScore = boTest.TestScore;
            TestersNote = boTest.TestersNote;
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
