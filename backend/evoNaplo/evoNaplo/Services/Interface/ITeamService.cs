using evoNaplo.Services;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    public interface ITeamService
    {
        List<Team> GetAllTeams();
        Team GetTeamById(string id);
        void AddTeam(Team team);
        void UpdateTeam(string id, Team updatedTeam);
        void DeleteTeam(string id);
    }
}