using System.Diagnostics.CodeAnalysis;

namespace evoNaplo.DTO;

public class ProjectDTO
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Dictionary<string, string> ProjectLinks { get; set; }
    public required IEnumerable<string> TeamIds { get; set; }

    public ProjectDTO(Project project)
    {
        Id = project.Id;
        Name = project.Name;
        Description = project.ShortDescription;
        ProjectLinks = project.ProjectLinks.ToDictionary(l => l.LinkType.ToString(), l => l.Url);
        TeamIds = project.Teams.Select(t => t.Id).ToList();
        
    }

}
