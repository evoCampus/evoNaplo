namespace evoNaplo.Models
{
    public class Project
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? ShortDescription { get; set; }
        public required ICollection<ProjectLink> ProjectLinks { get; set; }
        public required ICollection<Team> Teams { get; set; }
    }
}
