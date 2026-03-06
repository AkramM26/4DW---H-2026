namespace LocationManagerCore.Domains
{
    public class Note
    {
        public  Guid Id { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }

    }
}