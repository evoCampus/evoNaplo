using evoNaplo.Services;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
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