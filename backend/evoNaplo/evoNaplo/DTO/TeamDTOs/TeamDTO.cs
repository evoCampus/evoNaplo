using System.Diagnostics.CodeAnalysis;
using evoNaplo.Models;

namespace evoNaplo.DTO.TeamDTOs;

public class TeamDTO
{
    public required string Id { get; set; }
    public string? ProjectId { get; set; }

    [SetsRequiredMembers]
    public TeamDTO(Team team)
    {
        Id = team.Id;
        ProjectId = team.ProjectId;
    }

}
