using LocationManagerCore.Domains;

namespace LocationManageCore.Domains
{
    public class Car
    {
        //Required elements 

        public Guid Id { get; set; }
        public required string CarBrand { get; set; }
        public required string CarModel { get; set; }
        public required int? Year { get; set; }
        public required string Nickname { get; set; }
        public required string SerialNumber { get; set; }
        public required int? Mileage { get; set; }

        public required string Color { get; set; }
        public required string Registration { get; set; }
        public required decimal? EstimatedValue { get; set; }

        //Not required elements

        public string Status { get; set; } = "Actif";
        public bool Availability { get; set; }
        public bool State { get; set; }

        //Propriétés de navigation 

        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

        public static Car Create(
            string carbrand,
            string carmodel,
            int year,
            string color,
            string serialnumber,
            string registration,
            int mileage,
            string nickname,
            decimal estimatedvalue,
            Guid branchid)
        {
            return new Car
            {
                Id = Guid.NewGuid(),
                CarBrand = carbrand,
                CarModel = carmodel,
                Year = year,
                Color = color,
                SerialNumber = serialnumber,
                Registration = registration,
                Mileage = mileage,
                Nickname = nickname,
                EstimatedValue = estimatedvalue,
                BranchId = branchid,
                Availability = true
            };
        }
    }
}
