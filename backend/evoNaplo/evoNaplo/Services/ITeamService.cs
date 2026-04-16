using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;
namespace evoNaplo.Services
{
    internal interface ITeamService
    {
        IEnumerable<Team> GetAllTeams();
        Team GetTeamById(string id);
        void AddTeam(Team team);
        void UpdateTeam(string id, Team updatedTeam);
        void DeleteTeam(string id);
    }
}