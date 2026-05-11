using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagerCore.Domains
{
    public class AppUser : IdentityUser<Guid>
    {
        private AppUser() : base() { }
        public required string FullName { get; set; }

        public Guid? BranchId { get; set; }

        [NotMapped]
        public string LastName { get; set; }

        [NotMapped]
        public string FirstName { get; set; }

        [NotMapped]
        public string? EmailAddress { get; init; }

        [NotMapped]
        public string Role { get; set; }

        private AppUser(string username) : base (username) { }

        public static AppUser Create(
           string userName,
           string fullName,
           string email)
        {
            return new(userName)
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                Email = email,
                NormalizedEmail = email.ToUpper()
            };
        }
    }
}
