using evoNaplo.Services;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    public interface IProjectService
    {
        List<Project> GetAllProjects();
        Project GetProjectById(string id);
        void AddProject(Project project);
        void UpdateProject(Project project);
        void UpdateProjectFields(string id, Project updatedFields);

        void UpdateProjectName(string id, string name);
        void UpdateProjectDescription(string id, string description);
        void UpdateProjectLinks(string id, string links);
        void UpdateProjectAssignedMentors(string id, string mentors);
        void UpdateProjectAssignedStudents(string id, string students);

        void DeleteProject(string id);
    }
}