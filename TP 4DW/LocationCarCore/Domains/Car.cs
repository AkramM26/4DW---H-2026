using LocationManagerCore.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        //Required elements 

        public Guid Id { get; set; }
        public required string CarBrand { get; set; }
        public required string CarModel { get; set; }
        public required int? Year { get; set; }
        public required string Nickname { get; set; }
        public required string SerialNumber { get; set; }
        public required int? Mileage { get; set; }

        public required string Color { get; set; }
        public required string Registration { get; set; }
        public required decimal? EstimatedValue { get; set; }


        //Not required elements
        
        public  bool Status { get; set; }        
        public  bool Availability { get; set; }
        public  bool State { get; set; }


        //Propriétés de navigation 

        public  Guid BranchId { get; set; }
        public  virtual Branch Branch { get; set; }







        public static Car Create(
            string carbrand,
            string carmodel,
            int year,
            string color,
            string serialnumber,
            string registration,
            int mileage,
            string nickname,
            decimal estimatedvalue)
        {
            return new Car
            {
                Id = Guid.NewGuid(),
                CarBrand = carbrand,
                CarModel = carmodel,
                Year = year,
                Color = color,
                SerialNumber = serialnumber,
                Registration = registration,
                Mileage = mileage,
                Nickname = nickname,
                EstimatedValue = estimatedvalue
            };
        }
    }
}
