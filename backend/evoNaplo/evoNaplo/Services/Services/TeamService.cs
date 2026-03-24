using evoNaplo.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace evoNaplo.Services.Services
{
    public class TeamService : Interface.ITeamService
    {
        private static readonly List<Team> _teams = new List<Team>();
        private static int _nextId = 1;

        public List<Team> GetAllTeams()
        {
            return _teams;
        }

        public Team GetTeamById(int id)
        {
            return _teams.FirstOrDefault(t => t.TeamID == id);
        }

        public void AddTeam(Team team)
        {
            team.TeamID = _nextId++;
            _teams.Add(team);
        }

        public void UpdateTeam(Team team)
        {
            var existing = _teams.FirstOrDefault(t => t.TeamID == team.TeamID);
            if (existing == null) return;

            existing.TeamAssignedMentors = team.TeamAssignedMentors;
            existing.TeamAssignedStudents = team.TeamAssignedStudents;
        }

        public void UpdateTeamFields(int id, Team updatedFields)
        {
            var existing = _teams.FirstOrDefault(t => t.TeamID == id);
            if (existing == null || updatedFields == null) return;

            if (updatedFields.TeamAssignedMentors != null) existing.TeamAssignedMentors = updatedFields.TeamAssignedMentors;
            if (updatedFields.TeamAssignedStudents != null) existing.TeamAssignedStudents = updatedFields.TeamAssignedStudents;
        }

        public void UpdateTeamAssignedMentors(int id, string mentors)
        {
            var existing = _teams.FirstOrDefault(t => t.TeamID == id);
            if (existing == null) return;
            existing.TeamAssignedMentors = mentors;
        }

        public void UpdateTeamAssignedStudents(int id, string students)
        {
            var existing = _teams.FirstOrDefault(t => t.TeamID == id);
            if (existing == null) return;
            existing.TeamAssignedStudents = students;
        }

        public void DeleteTeam(int id)
        {
            var existing = _teams.FirstOrDefault(t => t.TeamID == id);
            if (existing != null) _teams.Remove(existing);
        }
    }
}