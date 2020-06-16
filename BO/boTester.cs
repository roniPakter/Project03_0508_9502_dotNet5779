using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Tester
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }

        private Address address;
        public Address Address
        {
            get { return address; }
            set
            {
                address.City = value.City;
                address.HouseNumber = value.HouseNumber;
                address.Street = value.Street;
            }
        }

        public int Seniority { get; set; }
        public int MaxTestsPerWeek { get; set; }
        public VehicleType CarType { get; set; }
        public bool[,] TableSchedule { get; set; }
        public double MaxDistanceFromAddress { get; set; }
        public List<TesterTest> Tests { get; set; } 

        public Tester(int iD, string lastName, string firstName, DateTime dateOfBirth, Gender gender, string phone,
            Address address, int seniority, int maxTestsPerWeek, VehicleType carType, double maxDistanceFromAddress, List<TesterTest> tests)
        {
            ID = iD;
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Phone = phone;
            Address = address;
            Seniority = seniority;
            MaxTestsPerWeek = maxTestsPerWeek;
            CarType = carType;
            TableSchedule = new bool[5, 6];
            MaxDistanceFromAddress = maxDistanceFromAddress;
            Tests = tests;
        }
        public Tester(Tester sourceTester)
        {
            ID = sourceTester.ID;
            LastName = sourceTester.LastName;
            FirstName = sourceTester.FirstName;
            DateOfBirth = sourceTester.DateOfBirth;
            Gender = sourceTester.Gender;
            Phone = sourceTester.Phone;
            Address = sourceTester.Address;
            Seniority = sourceTester.Seniority;
            MaxTestsPerWeek = sourceTester.MaxTestsPerWeek;
            CarType = sourceTester.CarType;
            TableSchedule = new bool[5, 6];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    TableSchedule[i, j] = sourceTester.TableSchedule[i, j];
                }
            }

            MaxDistanceFromAddress = sourceTester.MaxDistanceFromAddress;
            Tests = sourceTester.Tests;
        }

        public Tester()
        {
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public override string ToString()
        {
            return (
                "Tester's name: " + FirstName + " " + LastName + "\n" +
                "Gender: " + Gender.ToString() + "\n" +
                "Tester's ID: " + ID.ToString() + "\n" +
                "Seniority: " + Seniority.ToString() + " years\n" +
                "Vehicle type: " + CarType.ToString() + "\n" +
                "Address: " + Address.ToString() + "\n" +
                "Phone: " + Phone);
        }
    }

    
}
