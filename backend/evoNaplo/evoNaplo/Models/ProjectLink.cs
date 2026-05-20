namespace evoNaplo.Models;

public enum LinkTypes
{
    GitHub,
    Trello,
    Figma
}

public class ProjectLink
{
    public required string Id { get; set; }
    public required LinkTypes LinkType { get; set; }
    public required string Url { get; set; }
    public required string ProjectId { get; set; }
    public Project Project { get; set; }
}