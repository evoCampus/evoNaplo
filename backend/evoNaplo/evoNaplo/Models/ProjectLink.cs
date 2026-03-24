namespace evoNaplo.Models
{
    public class ProjectLink
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public LinkTypes LinkType { get; set; }
        public string Url { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
