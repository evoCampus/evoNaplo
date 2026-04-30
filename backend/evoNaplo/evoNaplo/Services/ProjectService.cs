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

        /// <summary>
        /// Returns the Project model by its Id, or null if not found.
        /// </summary>
        /// <param name="id">The ID of the project to retrieve.</param>
        /// <returns>The Project model if found, otherwise null.</returns>
        public Project? GetProjectModelById(string id)
        {
            return _projects.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Returns a list of all projects as ProjectDTOs. If a project has null properties, they are replaced with "N/A" or empty collections in the DTO.
        /// </summary>
        /// <returns>A list of ProjectDTOs.</returns>
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

        /// <summary>
        /// Returns a single project as a ProjectDTO by its ID. If the project is not found, returns null. If the project has null properties, they are replaced with "N/A" or empty collections in the DTO.
        /// </summary>
        /// <param name="id">The ID of the project to retrieve.</param>
        /// <returns>The ProjectDTO if found, otherwise null.</returns>
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

        /// <summary>
        /// Adds a new project to the list. If the provided ProjectDTO does not have an ID, a new GUID is generated for it. The project is created with the provided name, description, and associated teams (if any). If any of the properties in the DTO are null, they are replaced with "N/A" or empty collections in the created Project model.
        /// </summary>
        /// <param name="projectToCreate">The ProjectDTO to add.</param>
        public void AddProject(ProjectDTO projectToCreate)
        {
            if (string.IsNullOrEmpty(projectToCreate.Id)) 
                projectToCreate.Id = System.Guid.NewGuid().ToString();
            Project newProject = new Project
            {
                Id = projectToCreate.Id,
                Name = projectToCreate.Name ?? "N/A",
                ShortDescription = projectToCreate.Description ?? "N/A",
                Teams = projectToCreate.TeamIds?.Select(id => _teamService.GetTeamModelById(id)).OfType<Team>().ToList() ?? new List<Team>()
            };
            _projects.Add(newProject);
        }

        /// <summary>
        /// Updates an existing project with the specified ID using the provided ProjectDTO. If the project is not found or the updatedProject is null, the method will return without making any changes. For each property in the updatedProject that is not null, the corresponding property of the existing project will be updated. The project's teams will be updated based on the provided team IDs, or set to an empty list if the IDs are null.
        /// </summary>
        /// <param name="id">The ID of the project to update.</param>
        /// <param name="updatedProject">The updated project DTO.</param>
        public void UpdateProject(string id, ProjectDTO updatedProject)
        {
            var existing = _projects.FirstOrDefault(p => p.Id == id);
            if (existing is null || updatedProject is null) return;

            if (updatedProject.Id is not null) 
                existing.Id = updatedProject.Id;
            if (updatedProject.Name is not null) 
                existing.Name = updatedProject.Name ?? "N/A";
            if (updatedProject.Description is not null) 
                existing.ShortDescription = updatedProject.Description ?? "N/A";
            if (updatedProject.TeamIds is not null) 
                existing.Teams = updatedProject.TeamIds?.Select(id => _teamService.GetTeamModelById(id)).OfType<Team>().ToList() ?? new List<Team>();
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

        /// <summary>
        /// Deletes the project with the specified ID from the list. If the project is not found, the method will return without making any changes.
        /// </summary>
        /// <param name="id">The ID of the project to delete.</param>
        public void DeleteProject(string id)
        {
            var existing = _projects.FirstOrDefault(p => p.Id == id);
            if (existing is not null) 
                _projects.Remove(existing);
        }
    }
}
