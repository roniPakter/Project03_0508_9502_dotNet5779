using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;

namespace BL
{
    public class Configuration
    {
        public static int MinimumTesterAge { get; private set; }
        public static int MinimumTraineeAge { get; private set; }
        public static int MaximumTestersAge { get; private set; }
        public static int DaysIntervalBetweenTests { get; private set; }
        public static int MinimumNumberOfLessons { get; private set; }
        public static int BeginningSerialTestNumber { get; private set; }
        public static int MinimumPercentsOfPositiveGrades { get; private set; }
        public static int TimeTestersBeginToWork { get; private set; }
        public static int DaysBeforeTestATestCanBeDeleted { get; private set; }

        public DateTime UpdateTime { get; private set; }
        public bool Initialized { get; private set; }

        public Configuration()
        {
            IDL dlObject = DL.Factory.GetDL("lists");
            Dictionary<string, object> config = dlObject.getConfig();

            MinimumTesterAge = (int)config["MinimumTesterAge"];
            MinimumTraineeAge = (int)config["MinimumTraineeAge"];
            MaximumTestersAge = (int)config["MaximumTestersAge"];
            DaysIntervalBetweenTests = (int)config["DaysIntervalBetweenTests"];
            MinimumNumberOfLessons = (int)config["MinimumNumberOfLessons"];
            BeginningSerialTestNumber = (int)config["BeginningSerialTestNumber"];
            MinimumPercentsOfPositiveGrades = (int)config["MinimumPercentsOfPositiveGrades"];
            TimeTestersBeginToWork = (int)config["TimeTestersBeginToWork"];
            DaysBeforeTestATestCanBeDeleted = (int)config["DaysBeforeTestATestCanBeDeleted"];

            UpdateTime = DateTime.Now;
            Initialized = true;
        }
    }
}
