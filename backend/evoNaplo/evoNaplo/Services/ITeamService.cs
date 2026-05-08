using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal interface ITeamService
    {
        Team? GetTeamModelById(string id);
        Task<IEnumerable<TeamDTO>> GetAllTeamsAsync();
        Task<TeamDTO> GetTeamByIdAsync(string id);
        Task<TeamDTO> AddTeamAsync(TeamDTO team);
        Task<TeamDTO> UpdateTeamAsync(string id, TeamDTO updatedTeam);
        Task<bool> DeleteTeamAsync(string id);

    }
}
