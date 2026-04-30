using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal interface ITeamService
    {
        Team? GetTeamModelById(string id);
        IEnumerable<TeamDTO> GetAllTeams();
        TeamDTO? GetTeamById(string id);
        void AddTeam(TeamDTO team);
        void UpdateTeam(string id, TeamDTO updatedTeam);
        void DeleteTeam(string id);

    }
}
