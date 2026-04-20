using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;
using evoNaplo.Models;

[ApiController]
[Route("api/[controller]")]
internal class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Retrieves a list of all projects in the system, returning their details as ProjectDTO objects.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="ProjectDTO"/> objects.</returns>
    [HttpGet]
    public Task<IEnumerable<ProjectDTO>> GetProjects()
    {
        return Task.FromResult(_projectService.GetAllProjects().Select(project => new ProjectDTO
        {
            Id = project.Id,
            Name = project.Name ?? "N/A",
            Description = project.Description ?? "N/A",
            ProjectLinks = project.ProjectLinks ?? new Dictionary<string, string>(),
            Teams = project.Teams?.Select(team => team.Name).ToList() ?? Enumerable.Empty<string>()
        }));
    }

    /// <summary>
    /// Retrieves the details of a specific project based on the provided identifier. If no project with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ProjectDTO"/> representing
    /// the requested project.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a project with the specified <paramref name="projectId"/> does not exist.</exception>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDTO>> GetProject(string projectId)
    {
        Project? project = _projectService.GetProjectById(projectId);
        if (project is null)
            return NotFound($"Project with id {projectId} not found.");
        return Ok(new ProjectDTO 
        { 
            Id = project.Id,
            Name = project.Name ?? "N/A",
            Description = project.Description ?? "N/A",
            ProjectLinks = project.ProjectLinks ?? new Dictionary<string, string>(),
            Teams = project.Teams?.Select(team => team.Name).ToList() ?? Enumerable.Empty<string>()
        });
    }

    /// <summary>
    /// Creates a new project in the system based on the provided project data. If a project with the same identifier already exists, a Conflict response is returned.
    /// </summary>
    /// <param name="projectToCreate">The project data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created project.</returns>
    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject(ProjectDTO projectToCreate)
    {
        if (_projectService.GetProjectById(projectToCreate.Id) is not null)
            return Conflict($"Project with ID {projectToCreate.Id} already exists.");
        Project newProject = new Project
        {
            Id = projectToCreate.Id,
            Name = projectToCreate.Name ?? "N/A",
            Description = projectToCreate.Description ?? "N/A",
            ProjectLinks = projectToCreate.ProjectLinks ?? new Dictionary<string, string>(),
            Teams = projectToCreate.Teams ?? new List<string>()
        };
        _projectService.AddProject(newProject);
        return Ok(newProject);
    }

    /// <summary>
    /// Updates the details of an existing project in the system based on the provided identifier and updated project data. If no project with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to update. Cannot be null or empty.</param>
    /// <param name="updatedProject">An object containing the updated project information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the project is not found.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProject(string projectId, ProjectDTO updatedProject)
    {
        if (_projectService.GetProjectById(projectId) is null)
            return NotFound($"Project with ID {projectId} not found.");
        Project project = new Project
        {
            Id = updatedProject.Id,
            Name = updatedProject.Name ?? "N/A",
            Description = updatedProject.Description ?? "N/A",
            ProjectLinks = updatedProject.ProjectLinks ?? new Dictionary<string, string>(),
            Teams = updatedProject.Teams ?? new List<string>()
        };
        _projectService.UpdateProject(projectId, project);
        return NoContent();
    }

    /// <summary>
    /// Deletes an existing project from the system based on the provided identifier. If no project with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the deletion is successful; otherwise, an HTTP 404 response if the project is not found.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(string projectId)
    {
        if (_projectService.GetProjectById(projectId) is null)
            return NotFound($"Project with ID {projectId} not found.");
        _projectService.DeleteProject(projectId);
        return NoContent();
    }
}
