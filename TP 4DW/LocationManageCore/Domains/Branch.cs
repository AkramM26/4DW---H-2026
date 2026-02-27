using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManageCore.Domains
{
    public class Branch
    {
        //Propriétés métiers 
        public Guid Id { get; private set; }
        public required bool Status { get; set; }
        public required string Name { get; set; }

        public static Branch Create(
            bool status,
            string name)
        {
            return new Branch
            {
                Id = Guid.NewGuid(),
                Status = status,
                Name = name
            };
        }
    }
}
