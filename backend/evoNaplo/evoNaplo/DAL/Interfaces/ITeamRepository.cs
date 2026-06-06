using evoNaplo.Models;

namespace evoNaplo.DAL.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team?> GetTeamByIdAsync(string id);
        Task AddTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(string id);
    }
}
