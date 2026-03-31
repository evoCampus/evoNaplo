using evoNaplo.Services;
using System.Collections.Generic;
using System.Linq;

namespace evoNaplo.Services.Services
{
    public class TeamService : Interface.ITeamService
    {
        private static readonly List<Team> _teams = new List<Team>();

        public List<Team> GetAllTeams()
        {
            return _teams;
        }

        public Team GetTeamById(string id)
        {
            return _teams.FirstOrDefault(t => t.TeamID == id);
        }

        public void AddTeam(Team team)
        {
            if (string.IsNullOrEmpty(team.TeamID)) team.TeamID = System.Guid.NewGuid().ToString();
            _teams.Add(team);
        }

       
        public void UpdateTeam(string id, Team updatedTeam)
        {
            var existing = _teams.FirstOrDefault(t => t.TeamID == id);
            if (existing is null || updatedTeam is null) return;

            existing.TeamAssignedMentors = updatedTeam.TeamAssignedMentors;
            existing.TeamAssignedStudents = updatedTeam.TeamAssignedStudents;
        }

        public void UpdateTeamFields(string id, Team updatedFields)
        {
            var existing = _teams.FirstOrDefault(t => t.TeamID == id);
            if (existing is null || updatedFields is null) return;

            if (updatedFields.TeamAssignedMentors != null) existing.TeamAssignedMentors = updatedFields.TeamAssignedMentors;
            if (updatedFields.TeamAssignedStudents != null) existing.TeamAssignedStudents = updatedFields.TeamAssignedStudents;
        }

        

        public void DeleteTeam(string id)
        {
            var existing = _teams.FirstOrDefault(t => t.TeamID == id);
            if (existing is not null) _teams.Remove(existing);
        }
    }
}