using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;

namespace evoNaplo.Services
{
    internal interface IProjectService
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProjectById(string id);
        void AddProject(Project project);
        void UpdateProject(string id, Project updatedProject);
        void AddTeamsToProject(string projectId, IEnumerable<Team> teams);
        void RemoveTeamsFromProject(string projectId, IEnumerable<Team> teams);
        void DeleteProject(string id);
    }
}