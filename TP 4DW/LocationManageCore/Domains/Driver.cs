using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManageCore.Domains
{
    public class Driver
    {
        public Guid Id { get; set; }

        public required string LastName { get; set; }

        public required string FirstName { get; set; }

        public required string EmailAdress { get; set; }

        public required string PhoneNumber { get; set; }
         
        public required string DriverLicenceNumber { get; set; } 

        public static Driver Create(
            string firstname,
            string lastname,
            string emailadress,
            string phonenumber,
            string driverlicensenumber)
        {
            return new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = firstname,
                LastName = lastname,
                EmailAdress = emailadress,
                PhoneNumber = phonenumber,
                DriverLicenceNumber = driverlicensenumber
            };
        }

    }
}
