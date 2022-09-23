namespace Infrastructure.Entities
{
    public class Reader : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
    }
}
