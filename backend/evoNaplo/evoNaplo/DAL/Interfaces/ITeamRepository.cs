using evoNaplo.Models;

namespace evoNaplo.DAL.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team?> GetTeamByIdAsync(string id);
        Task<Team> AddTeamAsync(Team team);
        Task<Team> UpdateTeamAsync(Team team);
        Task<bool> DeleteTeamAsync(string id);
    }
}
