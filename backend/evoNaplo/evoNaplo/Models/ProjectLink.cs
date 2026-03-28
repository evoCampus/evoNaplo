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
        public string Id { get; set; }
        public LinkTypes LinkType { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
