using evoNaplo.Services;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    public interface ITeamService
    {
        List<Team> GetAllTeams();
        Team GetTeamById(string id);
        void AddTeam(Team team);
        void UpdateTeam(Team team);
        void UpdateTeamFields(string id, Team updatedFields);

        void UpdateTeamAssignedMentors(string id, string mentors);
        void UpdateTeamAssignedStudents(string id, string students);

        void DeleteTeam(string id);
    }
}