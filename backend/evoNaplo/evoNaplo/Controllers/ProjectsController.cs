using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;

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
    /// Retrieves a list of all projects in the system.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="ProjectDTO"/> objects representing all projects in the system.</returns>
    [HttpGet]
    public Task<IEnumerable<ProjectDTO>> GetProjects()
    {
        return Task.FromResult(_projectService.GetAllProjects());
    }

    /// <summary>
    /// Retrieves the details of a specific project based on the provided identifier. If a project with the given identifier exists, its details are returned as a <see cref="ProjectDTO"/> object. If no project is found with the specified identifier, a NotFound response is returned.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ProjectDTO"/> representing the requested project or a NotFound response.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDTO>> GetProject(string projectId)
    {
        ProjectDTO? project = _projectService.GetProjectById(projectId);
        if (project is not null)
            return Ok(project);
        else
            return NotFound($"Project with id {projectId} not found.");
    }

    /// <summary>
    /// Creates a new project in the system based on the provided project data. If a project with the same identifier already exists, a Conflict response is returned. Otherwise, the new project is added to the system and an Ok response containing the created project data is returned.
    /// </summary>
    /// <param name="projectToCreate">The project data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created project.</returns>
    [HttpPost]
    public async Task<ActionResult<ProjectDTO>> CreateProject(ProjectDTO projectToCreate)
    {
        if (_projectService.GetProjectById(projectToCreate.Id) is null)
        {
            _projectService.AddProject(projectToCreate);
            return Ok(projectToCreate);
        }
        else
            return Conflict($"Project with ID {projectToCreate.Id} already exists.");
    }

    /// <summary>
    /// Updates an existing project in the system based on the provided identifier and updated project data. If a project with the specified identifier exists, it is updated with the new data. If no project is found with the given identifier, a NotFound response is returned.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to update. Cannot be null or empty.</param>
    /// <param name="updatedProject">An object containing the updated project information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the project is not found.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProject(string projectId, ProjectDTO updatedProject)
    {
        if (_projectService.GetProjectById(projectId) is not null)
        {
            _projectService.UpdateProject(projectId, updatedProject);
            return NoContent();
        }
        else
            return NotFound($"Project with ID {projectId} not found.");
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
