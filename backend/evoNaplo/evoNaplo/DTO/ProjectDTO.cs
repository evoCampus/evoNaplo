using System.Diagnostics.CodeAnalysis;
using evoNaplo.Models;

namespace evoNaplo.DTO;

public class ProjectDTO
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Dictionary<string, string> ProjectLinks { get; set; } = new Dictionary<string, string>();
    public required IEnumerable<string> TeamIds { get; set; } = new List<string>();

    [SetsRequiredMembers]
    public ProjectDTO()
    {
    }
        [SetsRequiredMembers]
    public ProjectDTO(Project project)
    {
        Id = project.Id;
        Name = project.Name;
        Description = project.ShortDescription;
        ProjectLinks = project.ProjectLinks?.DistinctBy(l => l.LinkType).ToDictionary(l => l.LinkType.ToString(), l => l.Url) ?? new Dictionary<string, string>();
        TeamIds = project.Teams?.Select(t => t.Id).ToList() ?? new List<string>();
        
    }

}
