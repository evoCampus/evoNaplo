using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;
namespace evoNaplo.Services
{
    internal class ProjectService : IProjectService
    {
        private static readonly List<Project> _projects = new List<Project>();

        public IEnumerable<Project> GetAllProjects()
        {
            return _projects;
        }

        public Project GetProjectById(string id)
        {
            return _projects.FirstOrDefault(p => p.ProjectID == id);
        }

        public void AddProject(Project project)
        {
            if (string.IsNullOrEmpty(project.ProjectID))
            {
                project.ProjectID = System.Guid.NewGuid().ToString();
            }
            _projects.Add(project);
        }
        public void UpdateProject(string id, Project updatedProject)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing is null || updatedProject is null) return;

            if (updatedProject.ProjectName is not null) existing.ProjectName = updatedProject.ProjectName;
            if (updatedProject.ProjectDescription is not null) existing.ProjectDescription = updatedProject.ProjectDescription;
            if (updatedProject.ProjectLinks is not null) existing.ProjectLinks = updatedProject.ProjectLinks;
            if (updatedProject.ProjectAssignedMentors is not null) existing.ProjectAssignedMentors = updatedProject.ProjectAssignedMentors;
            if (updatedProject.ProjectAssignedStudents is not null) existing.ProjectAssignedStudents = updatedProject.ProjectAssignedStudents;
            if (updatedProject.Teams is not null) 
            {
                // Replace the project's teams with the provided collection
                existing.Teams = updatedProject.Teams;
            }
        }

        public void AddTeamsToProject(string projectId, IEnumerable<Team> teams)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == projectId);
            if (existing is null || teams is null) return;

            if (existing.Teams == null) existing.Teams = new List<Team>();

            foreach (var team in teams)
            {
                if (team is null) continue;
                // avoid duplicates by Team Id
                if (!existing.Teams.Any(t => t.TeamID == team.TeamID))
                {
                    existing.Teams.Add(team);
                }
            }
        }

        public void RemoveTeamsFromProject(string projectId, IEnumerable<Team> teams)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == projectId);
            if (existing is null || teams is null || existing.Teams is null) return;

            var idsToRemove = teams.Where(t => t is not null).Select(t => t.TeamID).ToHashSet();
            existing.Teams = existing.Teams.Where(t => !idsToRemove.Contains(t.TeamID)).ToList();
        }
        public void DeleteProject(string id)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing is not null) _projects.Remove(existing);
        }
    }
}