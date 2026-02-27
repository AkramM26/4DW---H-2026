using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Models
{
    public class Location
    {
        public Guid Id { get; set; }

        public required bool Status { get; set; }

        public required DateTime Opening { get; set; }

        public required DateTime PlanedClosing { get; set; }
        public required DateTime OfficialClosing { get; set; }


        public static Location Create(
            bool status,
            DateTime opening,
            DateTime planedclosing,
            DateTime officialclosure)

        {
            return new Location
            {
                Id = Guid.NewGuid(),
                Status = status,
                Opening = opening,
                PlanedClosing = planedclosing,
                OfficialClosing = officialclosure
            };
        }


    }
}
