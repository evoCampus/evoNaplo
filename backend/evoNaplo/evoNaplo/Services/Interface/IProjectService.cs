using evoNaplo.Services;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    internal interface IProjectService
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProjectById(string id);
        void AddProject(Project project);
        void UpdateProject(string id, Project updatedProject);
        void DeleteProject(string id);
    }
}