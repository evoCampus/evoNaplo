using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;
using evoNaplo.Models;
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
            return _projects.FirstOrDefault(p => p.Id == id);
        }

        public void AddProject(Project project)
        {
            if (string.IsNullOrEmpty(project.Id))
            {
                project.Id = System.Guid.NewGuid().ToString();
            }
            _projects.Add(project);
        }
        public void UpdateProject(string id, Project updatedProject)
        {
            var existing = _projects.FirstOrDefault(p => p.Id == id);
            if (existing is null || updatedProject is null) return;

            if (updatedProject.Name is not null) existing.Name = updatedProject.Name;
            if (updatedProject.ShortDescription is not null) existing.ShortDescription = updatedProject.ShortDescription;
            if (updatedProject.ProjectLinks is not null) existing.ProjectLinks = updatedProject.ProjectLinks;
            //If needed later or used in the DB-s can be used again otherwise, if not needed , can be removed from the model and the service
            //if (updatedProject.ProjectAssignedMentors is not null) existing.ProjectAssignedMentors = updatedProject.ProjectAssignedMentors;
            //if (updatedProject.ProjectAssignedStudents is not null) existing.ProjectAssignedStudents = updatedProject.ProjectAssignedStudents;
            if (updatedProject.Teams is not null) 
            {
                existing.Teams = updatedProject.Teams;
            }
        }

        public void AddTeamsToProject(string projectId, IEnumerable<Team> teams)
        {
            var existing = _projects.FirstOrDefault(p => p.Id == projectId);
            if (existing is null || teams is null) return;

            if (existing.Teams == null) existing.Teams = new List<Team>();

            foreach (var team in teams)
            {
                if (team is null) continue;
                // avoid duplicates by Team Id
                if (!existing.Teams.Any(t => t.Id == team.Id))
                {
                    existing.Teams.Add(team);
                }
            }
        }

        public void RemoveTeamsFromProject(string projectId, IEnumerable<Team> teams)
        {
            var existing = _projects.FirstOrDefault(p => p.Id == projectId);
            if (existing is null || teams is null || existing.Teams is null) return;

            var idsToRemove = teams.Where(t => t is not null).Select(t => t.Id).ToHashSet();
            existing.Teams = existing.Teams.Where(t => !idsToRemove.Contains(t.Id)).ToList();
        }
        public void DeleteProject(string id)
        {
            var existing = _projects.FirstOrDefault(p => p.Id == id);
            if (existing is not null) _projects.Remove(existing);
        }
    }
}