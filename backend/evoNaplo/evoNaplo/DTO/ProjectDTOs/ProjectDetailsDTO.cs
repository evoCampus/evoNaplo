using evoNaplo.DTO.TeamDTOs;
using evoNaplo.Models;
using System.Diagnostics.CodeAnalysis;

namespace evoNaplo.DTO.ProjectDTOs
{
    public class ProjectDetailsDTO : ProjectDTO
    {
        public IEnumerable<TeamDTO> Teams { get; set; }

        [SetsRequiredMembers]
        public ProjectDetailsDTO(Project project) : base(project)
        {
            Teams = project.Teams?.Select(t => new TeamDTO(t)) ?? Enumerable.Empty<TeamDTO>();
        }
    }

}
