using evoNaplo.DTO.TeamDTOs;
using evoNaplo.Models;

namespace evoNaplo.Services;

public interface ITeamService
{
    Task<Team> GetTeamModelById(string id);
    Task<IEnumerable<TeamDTO>> GetAllTeamsAsync();
    Task<TeamDTO> GetTeamByIdAsync(string id);
    Task<TeamDTO> AddTeamAsync(CreateTeamDTO teamToAdd);
    Task<TeamDTO> UpdateTeamAsync(string id, UpdateTeamDTO updatedTeam);
    Task<bool> DeleteTeamAsync(string id);

}