using evoNaplo.Services.Models;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    public interface IProjectService
    {
        List<Project> GetAllProjects();
        Project GetProjectById(int id);
        void AddProject(Project project);
        void UpdateProject(Project project);
        void UpdateProjectFields(int id, Project updatedFields);

        void UpdateProjectName(int id, string name);
        void UpdateProjectDescription(int id, string description);
        void UpdateProjectLinks(int id, string links);
        void UpdateProjectAssignedMentors(int id, string mentors);
        void UpdateProjectAssignedStudents(int id, string students);

        void DeleteProject(int id);
    }
}