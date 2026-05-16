namespace evoNaplo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Retrieves a list of all projects in the database. If no projects are found, a NotFound response is returned with an appropriate message. Each project is returned as a ProjectDTO.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="ProjectDTO"/> objects representing all projects in the database.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
    {
        try
        {
            return Ok(await _projectService.GetAllProjectsAsync());
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a specific project by its ID. If a project with the specified ID is found, it is returned as a ProjectDTO. If no project is found with the given ID, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <param name="projectId">The ID of the project to retrieve.</param>
    /// <returns>The ProjectDTO if found, otherwise a NotFound response.</returns>
    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectDTO>> GetProject(string projectId)
    {
        try
        {
            return Ok(await _projectService.GetProjectByIdAsync(projectId));
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Adds a new project to the database. If a project with the same name already exists, a Conflict response is returned with an appropriate message. If the project is added successfully, the created ProjectDTO is returned in the response body.
    /// </summary>
    /// <param name="projectToCreate">The project data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created project.</returns>
    [HttpPost]
    public async Task<ActionResult<ProjectDTO>> CreateProject(ProjectDTO projectToCreate)
    {
        try
        {
            return Ok(await _projectService.AddProjectAsync(projectToCreate));
        }
        catch (ProjectAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing project in the database based on the provided identifier and the provided updated project data. If no project with the given identifier exists, a NotFound response is returned with an appropriate message. If the project is updated successfully, a NoContent response is returned.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to update. Cannot be null or empty.</param>
    /// <param name="updatedProject">An object containing the updated project information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the project is not found.</returns>
    [HttpPut("{projectId}")]
    public async Task<ActionResult> UpdateProject(string projectId, ProjectDTO updatedProject)
    {
        try
        {
            return Ok(await _projectService.UpdateProjectAsync(projectId, updatedProject));
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a project from the database based on the provided identifier. If no project with the given identifier exists, a NotFound response is returned with an appropriate message. If the project is deleted successfully, a NoContent response is returned.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the deletion is successful; otherwise, an HTTP 404 response if the project is not found.</returns>
    [HttpDelete("{projectId}")]
    public async Task<ActionResult> DeleteProject(string projectId)
    {
        try
        {
            return Ok(await _projectService.DeleteProjectAsync(projectId));
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
}
