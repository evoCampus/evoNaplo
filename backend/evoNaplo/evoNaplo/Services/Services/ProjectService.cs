using evoNaplo.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace evoNaplo.Services.Services
{
    public class ProjectService : Interface.IProjectService
    {
        private static readonly List<Project> _projects = new List<Project>();
        private static int _nextId = 1;

        public List<Project> GetAllProjects()
        {
            return _projects;
        }

        public Project GetProjectById(int id)
        {
            return _projects.FirstOrDefault(p => p.ProjectID == id);
        }

        public void AddProject(Project project)
        {
            project.ProjectID = _nextId++;
            _projects.Add(project);
        }

        public void UpdateProject(Project project)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == project.ProjectID);
            if (existing == null) return;

            existing.ProjectName = project.ProjectName;
            existing.ProjectDescription = project.ProjectDescription;
            existing.ProjectLinks = project.ProjectLinks;
            existing.ProjectAssignedMentors = project.ProjectAssignedMentors;
            existing.ProjectAssignedStudents = project.ProjectAssignedStudents;
        }

        public void UpdateProjectFields(int id, Project updatedFields)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing == null || updatedFields == null) return;

            if (updatedFields.ProjectName != null) existing.ProjectName = updatedFields.ProjectName;
            if (updatedFields.ProjectDescription != null) existing.ProjectDescription = updatedFields.ProjectDescription;
            if (updatedFields.ProjectLinks != null) existing.ProjectLinks = updatedFields.ProjectLinks;
            if (updatedFields.ProjectAssignedMentors != null) existing.ProjectAssignedMentors = updatedFields.ProjectAssignedMentors;
            if (updatedFields.ProjectAssignedStudents != null) existing.ProjectAssignedStudents = updatedFields.ProjectAssignedStudents;
        }

        public void UpdateProjectName(int id, string name)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing == null) return;
            existing.ProjectName = name;
        }

        public void UpdateProjectDescription(int id, string description)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing == null) return;
            existing.ProjectDescription = description;
        }

        public void UpdateProjectLinks(int id, string links)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing == null) return;
            existing.ProjectLinks = links;
        }

        public void UpdateProjectAssignedMentors(int id, string mentors)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing == null) return;
            existing.ProjectAssignedMentors = mentors;
        }

        public void UpdateProjectAssignedStudents(int id, string students)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing == null) return;
            existing.ProjectAssignedStudents = students;
        }

        public void DeleteProject(int id)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectID == id);
            if (existing != null) _projects.Remove(existing);
        }
    }
}