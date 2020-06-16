using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public struct Address
    {
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string City { get; set; }

        public Address(string streetName, int number, string city)
        {
            Street = streetName;
            HouseNumber = number;
            City = city;
        }
        
        public override string ToString()
        {
            return Street + " " + HouseNumber.ToString() + ", " + City;
        }
    }
}
