using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;
using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal class ProjectService : IProjectService
    {
        private static readonly List<Project> _projects = new List<Project>();
        private readonly ITeamService _teamService;

        public ProjectService(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            return _projects.Select(project => new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name ?? "N/A",
                Description = project.ShortDescription ?? "N/A",
                ProjectLinks = project.ProjectLinks?.ToDictionary(link => link.LinkType.ToString(), link => link.Url) ?? new Dictionary<string, string>(),
                TeamIds = project.Teams?.Select(team => team.Id).ToList() ?? new List<string>(),
            });
        }

        public ProjectDTO? GetProjectById(string id)
        {
            Project? project = _projects.FirstOrDefault(p => p.Id == id);
            if (project is not null) 
                return new ProjectDTO 
                {
                    Id = project.Id,
                    Name = project.Name ?? "N/A",
                    Description = project.ShortDescription ?? "N/A",
                    ProjectLinks = project.ProjectLinks?.ToDictionary(link => link.LinkType.ToString(), link => link.Url) ?? new Dictionary<string, string>(),
                    TeamIds = project.Teams?.Select(team => team.Id).ToList() ?? new List<string>()
                };
            else
                return null;
        }

        public void AddProject(ProjectDTO projectToCreate)
        {
            if (string.IsNullOrEmpty(projectToCreate.Id)) 
                projectToCreate.Id = System.Guid.NewGuid().ToString();
            Project newProject = new Project
            {
                Id = projectToCreate.Id,
                Name = projectToCreate.Name ?? "N/A",
                ShortDescription = projectToCreate.Description ?? "N/A",
                Teams = projectToCreate.TeamIds?.Select(id => _teamService.GetTeamById(id)).OfType<Team>().ToList() ?? new List<Team>()
            };
            _projects.Add(newProject);
        }
        public void UpdateProject(string id, ProjectDTO updatedProject)
        {
            var existing = _projects.FirstOrDefault(p => p.Id == id);
            if (existing is null || updatedProject is null) return;

            if (updatedProject.Id is not null) existing.Id = updatedProject.Id;
            if (updatedProject.Name is not null) existing.Name = updatedProject.Name ?? "N/A";
            if (updatedProject.Description is not null) existing.ShortDescription = updatedProject.Description ?? "N/A";
            if (updatedProject.TeamIds is not null) existing.Teams = updatedProject.TeamIds?.Select(id => _teamService.GetTeamById(id)).OfType<Team>().ToList() ?? new List<Team>();
            //if (updatedProject.ProjectLinks is not null) existing.ProjectLinks = updatedProject.ProjectLinks;
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