namespace evoNaplo.Services;

internal class ProjectService : IProjectService
{
    private static readonly List<Project> _projects = new List<Project>();
    private readonly ITeamService _teamService;

    public ProjectService(ITeamService teamService)
    {
        _teamService = teamService;
    }

    /// <summary>
    /// This method is used internally by the service to retrieve the Project model for operations that require it, such as adding or updating projects. It is not intended to be exposed directly to clients, as it returns the internal model rather than a DTO.
    /// </summary>
    /// <param name="id">The ID of the project to retrieve.</param>
    /// <returns>The Project model if found, otherwise null.</returns>
    public Project GetProjectModelById(string id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        if (project is null)
        {
            throw new ProjectNotFoundException($"Project with ID {id} not found.");
        }
        return project;
    }

    /// <summary>
    /// Retrieves a list of all projects in the database. If no projects are found, a ProjectNotFoundException is thrown with an appropriate message. Each project is returned as a ProjectDTO.
    /// </summary>
    /// <returns>A list of ProjectDTOs if projects are found.</returns>
    /// <exception cref="ProjectNotFoundException"></exception>
    public async Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync()
    {
        IEnumerable<ProjectDTO> projects = _projects.Select(p => new ProjectDTO(p));
        return projects;
    }

    /// <summary>
    /// Retrieves a specific project by its ID. If a project with the specified ID is found, it is returned as a ProjectDTO. If no project is found with the given ID, a ProjectNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the project to retrieve.</param>
    /// <returns>The ProjectDTO if found.</returns>
    /// <exception cref="ProjectNotFoundException"></exception>
    public async Task<ProjectDTO> GetProjectByIdAsync(string id)
    {
        Project? project = _projects.FirstOrDefault(p => p.Id == id);
        if (project is not null) 
        {
            return new ProjectDTO(project);
        }
        throw new ProjectNotFoundException($"Project with ID {id} not found.");
    }

    /// <summary>
    /// Adds a new project to the list of projects. If a project with the same ID already exists, a ProjectAlreadyExistsException is thrown with an appropriate message. If the project is added successfully, the provided ProjectDTO is returned. If the ID of the project to add is null or empty, a new GUID will be generated and assigned as the ID.
    /// </summary>
    /// <param name="projectToAdd">The ProjectDTO to add.</param>
    /// <returns>The added ProjectDTO if successful.</returns>
    /// <exception cref="ProjectAlreadyExistsException"></exception>
    public async Task<ProjectDTO> AddProjectAsync(ProjectDTO projectToAdd)
    {
        projectToAdd.Id = Guid.NewGuid().ToString();
        _projects.Add(new Project
        {
            Id = projectToAdd.Id,
            Name = projectToAdd.Name,
            ShortDescription = projectToAdd.Description,
            ProjectLinks = projectToAdd.ProjectLinks.Select(l => new ProjectLink
            {
                Id = Guid.NewGuid().ToString(),
                LinkType = Enum.TryParse<LinkTypes>(l.Key, out var type) ? type : LinkTypes.GitHub,
                Url = l.Value,
                ProjectId = projectToAdd.Id
            }).ToList(),
            Teams = projectToAdd.TeamIds,
        });
        return projectToAdd;
    }

    /// <summary>
    /// Updates an existing project with the specified ID using the provided updated project data. If a project with the given ID is found, it is updated with the new data and the updated ProjectDTO is returned. If no project is found with the specified ID, a ProjectNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the project to update.</param>
    /// <param name="updatedProject">The updated project DTO.</param>
    /// <returns>The updated ProjectDTO if successful.</returns>
    /// <exception cref="ProjectNotFoundException"></exception>
    public async Task<ProjectDTO> UpdateProjectAsync(string id, ProjectDTO updatedProject)
    {
        var existing = _projects.FirstOrDefault(p => p.Id == id);
        if (existing is not null) 
        {
            existing.Id = updatedProject.Id;
            existing.Name = updatedProject.Name;
            existing.ShortDescription = updatedProject.Description;
            existing.ProjectLinks = updatedProject.ProjectLinks.Select(l => new ProjectLink
            {
                Id = Guid.NewGuid().ToString(),
                LinkType = Enum.TryParse<LinkTypes>(l.Key, out var type) ? type : LinkTypes.GitHub,
                Url = l.Value,
                ProjectId = updatedProject.Id
            }).ToList();
            existing.Teams = updatedProject.TeamIds;
            return updatedProject;
        }
        throw new ProjectNotFoundException($"Project with ID {id} not found.");
    }
    
    /// <summary>
    /// Deletes a project with the specified ID. If a project with the given ID is found, it is removed from the list of projects and the method returns true. If no project is found with the specified ID, a ProjectNotFoundException is thrown with an appropriate message and the method returns false.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    /// <returns>A boolean indicating whether the project was deleted.</returns>
    /// <exception cref="ProjectNotFoundException"></exception>
    public async Task<bool> DeleteProjectAsync(string id)
    {
        var existing = _projects.FirstOrDefault(p => p.Id == id);
        if (existing is not null) 
        {
            _projects.Remove(existing);
            return true;
        }
        throw new ProjectNotFoundException($"Project with ID {id} not found.");
    }
    
}
