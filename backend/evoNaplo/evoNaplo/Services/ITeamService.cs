using evoNaplo.DTO;
using evoNaplo.Models;

namespace evoNaplo.Services;

public interface ITeamService
{
    Team? GetTeamModelById(string id);
    Task<IEnumerable<TeamDTO>> GetAllTeamsAsync();
    Task<TeamDTO> GetTeamByIdAsync(string id);
    Task<TeamDTO> AddTeamAsync(TeamDTO teamToAdd);
    Task<TeamDTO> UpdateTeamAsync(string id, TeamDTO updatedTeam);
    Task<bool> DeleteTeamAsync(string id);

}