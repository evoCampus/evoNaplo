using System.Collections.Generic;
using System.Linq;
using evoNaplo.Models;
using evoNaplo.Services;
namespace evoNaplo.Services
{
    internal class TeamService : ITeamService
    {
        private static readonly List<Team> _teams = new List<Team>();

        public IEnumerable<Team> GetAllTeams()
        {
            return _teams;
        }

        public Team GetTeamById(string id)
        {
            return _teams.First(t => t.Id == id);
        }

        public void AddTeam(Team team)
        {
            if (string.IsNullOrEmpty(team.Id)) team.Id = System.Guid.NewGuid().ToString();
            _teams.Add(team);
        }

       
        public void UpdateTeam(string id, Team updatedTeam)
        {
            var existing = _teams.FirstOrDefault(t => t.Id == id);
            if (existing is null || updatedTeam is null) return;

            if (updatedTeam.ProjectId is not null) existing.ProjectId = updatedTeam.ProjectId;
            if (updatedTeam.Project is not null) existing.Project = updatedTeam.Project;
            if (updatedTeam.Mentors is not null) existing.Mentors = updatedTeam.Mentors;
            if (updatedTeam.Students is not null) existing.Students = updatedTeam.Students;
            if (updatedTeam.AttendanceSheets is not null) existing.AttendanceSheets = updatedTeam.AttendanceSheets;
        }

        public void DeleteTeam(string id)
        {
            var existing = _teams.FirstOrDefault(t => t.Id == id);
            if (existing is not null) _teams.Remove(existing);
        }
    }
}


