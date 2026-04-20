namespace LocationManagerCore.Domains
{
    public class Note
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }

        public static Note Create(
            string description)
        {
            return new Note
            {
                Id = Guid.NewGuid(),
                Content = description,
                CreatedAt = DateTime.Now
            };
        }

    }
}