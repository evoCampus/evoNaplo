namespace evoNaplo.Models
{
    public class Mentor
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}