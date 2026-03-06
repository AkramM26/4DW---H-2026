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
        public Address Address { get; set; }
        public ICollection<Location>? Locations { get; set; }

    }
}