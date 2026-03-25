namespace evoNaplo.Models
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public ICollection<ProjectLink> ProjectLink { get; set; } = new List<ProjectLink>();
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<Mentor> Mentors { get; set; } = new List<Mentor>();
    }
}