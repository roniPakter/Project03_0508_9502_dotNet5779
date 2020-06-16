using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ExternalTrainee
    {
        public int ID;
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

        public ExternalTrainee() {}
        public ExternalTrainee(Trainee trainee)
        {
            ID = trainee.ID;
            LastName = trainee.LastName;
            FirstName = trainee.FirstName;
            DateOfBirth = trainee.DateOfBirth;
            Gender = trainee.Gender;
            Phone = trainee.Phone;
            Address = trainee.Address;
            CarType = trainee.CarType;
            GearBoxType = trainee.GearBoxType;
            DrivingSchoolName = trainee.DrivingSchoolName;
            TeacherName = trainee.TeacherName;
            NumberOfLessons = trainee.NumberOfLessons;
        }
    }
}

