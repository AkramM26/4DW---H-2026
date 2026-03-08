using LocationManageCore.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagerCore.Domains
{
    public class Branch
    {
        public Guid Id { get; set; }
        public required bool Status { get; set; }
        public required string Name { get; set; }

        // Propriétés de navigation

        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();


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
