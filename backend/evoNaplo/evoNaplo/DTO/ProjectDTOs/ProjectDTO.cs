using evoNaplo.Models;
using System.Diagnostics.CodeAnalysis;

namespace evoNaplo.DTO.ProjectDTOs;

public class ProjectDTO
{
    public required string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string> ProjectLinks { get; set; }

    [SetsRequiredMembers]
    public ProjectDTO(Project project)
    {
        Id = project.Id;
        Name = project.Name;
        Description = project.ShortDescription;
        ProjectLinks = project.ProjectLinks?.DistinctBy(l => l.LinkType).ToDictionary(l => l.LinkType.ToString(), l => l.Url) ?? new Dictionary<string, string>();
    }
}
