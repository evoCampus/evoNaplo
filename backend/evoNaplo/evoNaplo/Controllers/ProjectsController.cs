using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private static List<ProjectDTO> _projects = new List<ProjectDTO>();

    /// <summary>
    /// Retrieves a collection of all available projects.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of project
    /// data transfer objects. The collection is empty if no projects are available.</returns>
    [HttpGet]
    public Task<IEnumerable<ProjectDTO>> GetProjects()
    {
        return Task.FromResult((IEnumerable<ProjectDTO>)_projects);
    }

    /// <summary>
    /// Retrieves the project with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the project to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the project data as a ProjectDTO.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a project with the specified identifier is not found.</exception>
    [HttpGet("{id}")]
    public Task<ProjectDTO> GetProject(string id)
    {
        var project = _projects.FirstOrDefault(s => s.Id == id);
        if (project is null)
            throw new KeyNotFoundException($"Project with id {id} not found.");
        return Task.FromResult<ProjectDTO>(project);
    }

    /// <summary>
    /// Creates a new project and adds it to the collection.
    /// </summary>
    /// <param name="project">The project to add. Must not be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created project.</returns>
    [HttpPost]
    public Task<ProjectDTO> CreateProject(ProjectDTO project)
    {
        _projects.Add(project);
        return Task.FromResult<ProjectDTO>(project);
    }

    /// <summary>
    /// Updates an existing project with the specified identifier using the provided project data.
    /// </summary>
    /// <param name="id">The unique identifier of the project to update. Cannot be null or empty.</param>
    /// <param name="updatedProject">The updated project data to apply. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. Returns a 204 No Content response if the update is
    /// successful; otherwise, returns a 404 Not Found response if the project does not exist.</returns>
    [HttpPut("{id}")]
    public Task UpdateProject(string id, ProjectDTO updatedProject)
    {
        var index = _projects.FindIndex(s => s.Id == id);
        if (index == -1)
            return Task.FromResult(NotFound());
        _projects[index] = updatedProject;
        return Task.FromResult(NoContent());
    }

    /// <summary>
    /// Deletes the project with the specified identifier.
    /// </summary>
    /// <remarks>If the specified project does not exist, the operation completes without error and no action
    /// is taken. This method is typically used in response to an HTTP DELETE request.</remarks>
    /// <param name="id">The unique identifier of the project to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    [HttpDelete("{id}")]
    public Task DeleteProject(string id)
    {
        var project = _projects.FirstOrDefault(s => s.Id == id);
        _projects.Remove(project);
        return Task.FromResult(NoContent());
    }
}
