using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Models
{
    public class User
    {
        public  Guid Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }= string.Empty;

        public static User Create(
            string username,
            string password)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                UserName = username,
                Password = password
            };
        }
    }
}
