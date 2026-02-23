using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LocationManageCore.Domains
{
    public class Car
    {
        public Guid Id { get; set; }

        public required string Nickname { get; set; }

        public required bool Status{ get; set; }

        public required bool Availability{ get; set; }

        public required bool State { get; set; }

        public required string SerialNumber { get; set; }

        public required string Registration{ get; set; }

        public required string  CarBrand{ get; set; }

        public required string CarModel{ get; set; }

        public required int Year { get; set; }

        public required string Color{ get; set; }

        public  required int Mileage{ get; set; }

        public required decimal EstimatedValue { get; set; }

        public static Car Create(
            string nickname,
            bool status,
            bool availability,
            bool state,
            string serialnumber,
            string registration,
            string carbrand,
            string carmodel,
            int year,
            string color,
            int mileage,
            decimal estimatedvalue)
        {
            return new Car
            {
                Id = Guid.NewGuid(),
                Nickname = nickname,
                Status = status,
                Availability = availability,
                State = state,
                SerialNumber = serialnumber,
                Registration = registration,
                CarBrand = carbrand,
                CarModel = carmodel,
                Year = year,
                Color = color,
                Mileage = mileage,
                EstimatedValue = estimatedvalue
            };
        }
    }
}
