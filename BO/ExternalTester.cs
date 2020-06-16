using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class ExternalTester
    {
        public readonly int ID;
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public VehicleType CarType { get; set; }

        public ExternalTester() {}
        
        public ExternalTester(Tester boTester)
        {
            ID = boTester.ID;
            LastName = boTester.LastName;
            FirstName = boTester.FirstName;
            CarType = boTester.CarType;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " ID: " + ID.ToString();
        }
    }
}
