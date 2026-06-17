using Microsoft.AspNetCore.Mvc;
using evoNaplo.Services;
using evoNaplo.DTO;
using evoNaplo.Exceptions;
using System.Net.Mime;

namespace evoNaplo.Controllers;

/// <summary>
/// Controller for managing projects in the application. Provides endpoints for retrieving, creating, updating, and deleting project records. Each endpoint interacts with the IProjectService to perform the necessary operations on the project data. The controller handles exceptions such as ProjectNotFoundException and ProjectAlreadyExistsException to return appropriate HTTP responses based on the outcome of each operation.
/// </summary>
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
    /// Retrieves a list of all projects. If projects are found, they are returned as a list of ProjectDTO objects. If no projects are found, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <returns>A list of ProjectDTO objects if projects are found; otherwise, a NotFound response.</returns>
    [HttpGet(Name = nameof(GetProjects))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// Retrieves a specific project by its unique identifier. If a project with the given identifier exists, it is returned as a ProjectDTO object. If no project is found with the provided identifier, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to retrieve.</param>
    /// <returns>The ProjectDTO if found; otherwise, a NotFound response.</returns>
    [HttpGet("{projectId}", Name = nameof(GetProject))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// Creates a new project in the database based on the provided ProjectDTO object. If a project with the same unique identifier already exists, a Conflict response is returned with an appropriate message. If the project is created successfully, the newly created ProjectDTO object is returned in the response.
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
    /// Updates an existing project in the database based on the provided identifier and updated ProjectDTO object. If no project with the given identifier exists, a NotFound response is returned with an appropriate message. If the project is updated successfully, the updated ProjectDTO object is returned in the response.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to update. Cannot be null or empty.</param>
    /// <param name="updatedProject">An object containing the updated project information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated project.</returns>
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
    /// Deletes a project from the database based on the provided identifier. If no project with the given identifier exists, a NotFound response is returned with an appropriate message. If the project is deleted successfully, an Ok response is returned indicating the successful deletion.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 200 response if the deletion is successful; otherwise, an HTTP 404 response if the project is not found.</returns>
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
