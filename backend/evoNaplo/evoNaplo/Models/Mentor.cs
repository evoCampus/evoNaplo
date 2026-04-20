namespace evoNaplo.Models
{
    public class Mentor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int TeamCount { get; set; }
        public int StudentCount { get; set; }
        public int ProjectCount { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}