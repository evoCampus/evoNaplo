using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;
using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal interface IProjectService
    {
        IEnumerable<ProjectDTO> GetAllProjects();
        ProjectDTO? GetProjectById(string id);
        void AddProject(ProjectDTO project);
        void UpdateProject(string id, ProjectDTO updatedProject);
        void AddTeamsToProject(string projectId, IEnumerable<Team> teams);
        void RemoveTeamsFromProject(string projectId, IEnumerable<Team> teams);
        void DeleteProject(string id);
    }
}