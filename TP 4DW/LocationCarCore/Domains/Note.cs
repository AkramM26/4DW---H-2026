namespace LocationManagerCore.Domains
{
    public class Note
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }
        public Guid? CarId { get; set; }
        public Guid? LocationId { get; set; }

        public static Note Create(string description)
        {
            return new Note
            {
                Id = Guid.NewGuid(),
                Content = description,
                CreatedAt = DateTime.Now
            };
        }

        public static Note CreateForCar(string description, Guid carId)
        {
            return new Note
            {
                Id = Guid.NewGuid(),
                Content = description,
                CreatedAt = DateTime.Now,
                CarId = carId
            };
        }
    }
}