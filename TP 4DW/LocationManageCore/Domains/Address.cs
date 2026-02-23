using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManageCore.Domains
{
    public class Address
    {
        public Guid Id { get; set; }

        public required string StreetNumber { get; set; }

        public required string CityName { get; set; }

        public required string PostalCode { get; set; }

        public static Address Create(
            string streetnumber,
            string cityname,
            string postalcode
            )
        {
            //Validations des paramètres (invariants)

            ArgumentException.ThrowIfNullOrWhiteSpace(streetnumber, nameof(StreetNumber));
            ArgumentException.ThrowIfNullOrWhiteSpace(cityname, nameof(CityName));
            ArgumentException.ThrowIfNullOrWhiteSpace(postalcode, nameof(PostalCode));

            return new Address
            {
                Id = Guid.NewGuid(),
                StreetNumber = streetnumber,
                CityName = cityname,
                PostalCode = postalcode
            };

        }

    }
}
