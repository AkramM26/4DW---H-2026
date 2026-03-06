using LocationManageCore.Domains;

namespace LocationManagerCore.Domains
{
    public class Location
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public DateTime Opening { get; set; }
        public DateTime PlanedClosing { get; set; }
        public DateTime? OfficialClosing { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }
        public ICollection<Note>? Notes { get; set; }


        public static Location Create(
            Guid carId, 
            Guid driverId,    
            DateTime opening,
            DateTime planedClosing)
        {
            return new Location
            {
                Id = Guid.NewGuid(),
                Status = true,                 // La location est ouverte au moment de la création
                Opening = opening,
                PlanedClosing = planedClosing,
                OfficialClosing = null,        // Pas encore fermée
                CarId = carId,
                DriverId = driverId,
                Notes = new List<Note>()       // Toujours initialiser les collections
            };
        }


    }


}