using evoNaplo.Servies;
using System.Collections.Generic;
using System.Linq;

namespace evoNaplo.Services.Services
{
    public class ProjectService : Interface.IProjectService
    {
        private static readonly List<Project> _projects = new List<Project>();

        public List<Project> GetAllProjects()
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

            existing.ProjectName = updatedProject.ProjectName;
            existing.ProjectDescription = updatedProject.ProjectDescription;
            existing.ProjectLinks = updatedProject.ProjectLinks;
            existing.ProjectAssignedMentors = updatedProject.ProjectAssignedMentors;
            existing.ProjectAssignedStudents = updatedProject.ProjectAssignedStudents;
        }

        public void UpdateProjectFields(string id, Project updatedFields)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing is null || updatedFields is null) return;

            if (updatedFields.ProjectName != null) existing.ProjectName = updatedFields.ProjectName;
            if (updatedFields.ProjectDescription != null) existing.ProjectDescription = updatedFields.ProjectDescription;
            if (updatedFields.ProjectLinks != null) existing.ProjectLinks = updatedFields.ProjectLinks;
            if (updatedFields.ProjectAssignedMentors != null) existing.ProjectAssignedMentors = updatedFields.ProjectAssignedMentors;
            if (updatedFields.ProjectAssignedStudents != null) existing.ProjectAssignedStudents = updatedFields.ProjectAssignedStudents;
        }

       

        public void DeleteProject(string id)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing is not null) _projects.Remove(existing);
        }
    }
}