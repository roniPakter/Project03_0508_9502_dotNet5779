using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class Trainee
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }

        public VehicleType CarType { get; set; }
        public GearBox GearBoxType { get; set; }
        public string DrivingSchoolName { get; set; }
        public string TeacherName { get; set; }
        public int NumberOfLessons { get; set; }
        //...

        public Trainee()
        {

        }
        public Trainee(int id, string lastName, string firstName,
            DateTime dateOfBirth, Gender gndr, string phone, Address addrs,
            VehicleType carType, GearBox gearBoxType, string drivingSchoolName,
            string teacherName, int numberOfLessons)
        {
            ID = id;
            Address = addrs;
            CarType = carType;
            DateOfBirth = dateOfBirth;
            DrivingSchoolName = drivingSchoolName;
            FirstName = firstName;
            GearBoxType = gearBoxType;
            Gender = gndr;
            LastName = lastName;
            NumberOfLessons = numberOfLessons;
            Phone = phone;
            TeacherName = teacherName;
        }

        [Obsolete]
        public Trainee(Trainee sourceTrainee)
        {
            ID = sourceTrainee.ID;
            Address = sourceTrainee.Address;
            CarType = sourceTrainee.CarType;
            DateOfBirth = sourceTrainee.DateOfBirth;
            DrivingSchoolName = sourceTrainee.DrivingSchoolName;
            FirstName = sourceTrainee.FirstName;
            GearBoxType = sourceTrainee.GearBoxType;
            Gender = sourceTrainee.Gender;
            LastName = sourceTrainee.LastName;
            NumberOfLessons = sourceTrainee.NumberOfLessons;
            Phone = sourceTrainee.Phone;
            TeacherName = sourceTrainee.TeacherName;
        }

        public override string ToString()
        {
            return (
                  "Trainee's name: " + FirstName + " " + LastName + "\n" +
                  "Gender: " + Gender.ToString() + "\n" +
                  "Trainee's ID: " + ID.ToString() + "\n" +
                  "Vehicle type: " + CarType.ToString() + "\n" +
                  "Transmission type: " + GearBoxType.ToString() + "\n" + 
                  "Number of lessons taken: " + NumberOfLessons.ToString() + "\n" +
                  "Address: " + Address.ToString() + "\n" +
                  "Phone: " + Phone);
        }
    }
}
