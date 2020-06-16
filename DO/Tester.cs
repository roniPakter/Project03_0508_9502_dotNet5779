using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Tester
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public int Seniority { get; set; }
        public int MaxTestsPerWeek { get; set; }
        public VehicleType CarType { get; set; }
        public double MaxDistanceFromAddress { get; set; }

        public Tester(int id, string lastName, string firstName, DateTime dateOfBirth, Gender gndr, string phone, Address adrs, int seniority, int maxTestsPerWeek, VehicleType carType, double maxDistanceFromAddress)
        {
            Address = adrs;
            CarType = carType;
            DateOfBirth = dateOfBirth;
            FirstName = firstName;
            Gender = gndr;
            ID = id;
            LastName = lastName;
            MaxDistanceFromAddress = maxDistanceFromAddress;
            MaxTestsPerWeek = maxTestsPerWeek;
            Phone = phone;
            Seniority = seniority;
        }
        [Obsolete]
        public Tester(Tester sourceTester)
        {
            Address = sourceTester.Address;
            CarType = sourceTester.CarType;
            DateOfBirth = sourceTester.DateOfBirth;
            FirstName = sourceTester.FirstName;
            Gender = sourceTester.Gender;
            ID = sourceTester.ID;
            LastName = sourceTester.LastName;
            MaxDistanceFromAddress = sourceTester.MaxDistanceFromAddress;
            MaxTestsPerWeek = sourceTester.MaxTestsPerWeek;
            Phone = sourceTester.Phone;
            Seniority = sourceTester.Seniority;
        }

        public override string ToString()
        {
            return (
                "Tester's name: " + FirstName + " " + LastName + "\n" + 
                "Gender: " + Gender.ToString() + "\n" +
                "Tester's ID: " + ID.ToString() + "\n" +
                "Seniority: " + Seniority.ToString() + " years\n" +
                "Vehicle type: " +  CarType.ToString() + "\n" +
                "Address: " + Address.ToString() + "\n" +
                "Phone: " + Phone);
        }
    }
}