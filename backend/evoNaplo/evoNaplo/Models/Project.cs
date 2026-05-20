using NanoidDotNet;

namespace evoNaplo.Models
{
    public class Project
    {
        public string Id { get; set; } = Nanoid.Generate();
        public string Name { get; set; }
        public string? ShortDescription { get; set; }
        public ICollection<ProjectLink> ProjectLinks { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}