using NanoidDotNet;

namespace evoNaplo.Models
{
    public enum LinkTypes
    {
        GitHub,
        Trello,
        Figma
    }
    public class ProjectLink
    {
        public string Id { get; set; } = Nanoid.Generate();
        public LinkTypes LinkType { get; set; }
        public string Url { get; set; }
        public string ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
