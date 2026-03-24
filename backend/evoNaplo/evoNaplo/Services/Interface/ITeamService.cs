using evoNaplo.Services.Models;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    public interface ITeamService
    {
        List<Team> GetAllTeams();
        Team GetTeamById(int id);
        void AddTeam(Team team);
        void UpdateTeam(Team team);
        void UpdateTeamFields(int id, Team updatedFields);

        void UpdateTeamAssignedMentors(int id, string mentors);
        void UpdateTeamAssignedStudents(int id, string students);

        void DeleteTeam(int id);
    }
}