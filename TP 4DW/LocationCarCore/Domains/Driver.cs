using LocationManageCore.Domains;
using System.Drawing;

namespace LocationManagerCore.Domains
{
    public class Driver
    {
        public Guid Id { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string EmailAdress { get; set; }
        public required string PhoneNumber { get; set; }
        public required string DriverLicenceNumber { get; set; }
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Location>? Locations { get; set; }

        public static Driver Create(string FirstName, string LastName, string EmailAdress, string driverLicenceNumber, string phoneNumber)
        {
            return new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = FirstName,
                LastName = LastName,
                EmailAdress = EmailAdress,
                DriverLicenceNumber = driverLicenceNumber,
                PhoneNumber = phoneNumber
            };
        }
    }
}