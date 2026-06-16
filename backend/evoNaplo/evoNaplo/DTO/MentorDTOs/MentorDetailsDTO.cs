using evoNaplo.DTO.ProjectDTOs;
using evoNaplo.DTO.TeamDTOs;
using evoNaplo.Models;
using System.Diagnostics.CodeAnalysis;

namespace evoNaplo.DTO.MentorDTOs;

public class MentorDetailsDTO : MentorDTO
{
    public IEnumerable<TeamDTO> Teams { get; set; }
    public IEnumerable<ProjectDTO> Projects { get; set; }

    [SetsRequiredMembers]
    public MentorDetailsDTO(Mentor mentor) : base(mentor)
    {
        Teams = mentor.Teams?.Select(t => new TeamDTO(t)) ?? Enumerable.Empty<TeamDTO>();
        Projects = mentor.Projects?.Select(p => new ProjectDTO(p)) ?? Enumerable.Empty<ProjectDTO>();
    }
}


