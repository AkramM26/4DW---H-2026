using System.Globalization;

namespace LocationManagerCore.Domains
{
    public class Address
    {
        public Guid Id { get; set; }
        public required string StreetNumber { get; set; }
        public required string StreetName { get; set; }
        public required string CityName { get; set; }
        public required string Province { get; set; }
        public required string Country { get; set; }
        public required string PostalCode { get; set; }

        public static Address Create(
            string streetnumber,
            string streetname,
            string cityname,
            string province,
            string country,
            string postalcode)
        {
            return new Address
            {
                Id = Guid.NewGuid(),
                StreetNumber = streetnumber,
                StreetName = streetname,
                CityName = cityname,
                Province = province,
                Country = country,
                PostalCode = postalcode

            };
        }

    }
}