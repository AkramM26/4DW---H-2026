using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagerCore.Domains
{
    public class AppUser : IdentityUser<Guid>
    {
        private AppUser() : base() {}

        private AppUser(string username) : base (username) { }

        public static AppUser Create(string username, string email)
        {
            return new(username)
            {
                Id = Guid.NewGuid(),
                Email = email,
                NormalizedEmail = email.ToUpper()
            };
        }
    }
}
