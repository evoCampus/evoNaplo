using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private static List<ProjectDTO> _projects = new List<ProjectDTO>();
    private static long _nextId = 1;

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
    /// <param name="id">The unique identifier of the project to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ActionResult{ProjectDTO}"/> representing the project data if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{id}")]
    public Task<ActionResult<ProjectDTO>> GetProject(long id)
    {
        var project = _projects.FirstOrDefault(s => s.Id == id);
        if (project == null)
            return Task.FromResult<ActionResult<ProjectDTO>>(NotFound());
        return Task.FromResult<ActionResult<ProjectDTO>>(Ok(project));
    }

    /// <summary>
    /// Creates a new project and adds it to the collection.
    /// </summary>
    /// <param name="project">The project data to create. The project's properties should be populated except for the identifier, which will
    /// be assigned by the server.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ActionResult that includes the
    /// created project data and a location header for retrieving the project by its identifier.</returns>
    [HttpPost]
    public Task<ActionResult<ProjectDTO>> CreateProject(ProjectDTO project)
    {
        project.Id = _nextId++;
        _projects.Add(project);
        return Task.FromResult<ActionResult<ProjectDTO>>(CreatedAtAction(nameof(GetProject), new { id = project.Id }, project));
    }

    /// <summary>
    /// Updates the project with the specified identifier using the provided project data.
    /// </summary>
    /// <param name="id">The unique identifier of the project to update.</param>
    /// <param name="updatedProject">The updated project data to apply to the existing project. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that
    /// is <see cref="NotFoundResult"/> if the project does not exist; otherwise, <see cref="NoContentResult"/> if the
    /// update is successful.</returns>
    [HttpPut("{id}")]
    public Task<IActionResult> UpdateProject(long id, ProjectDTO updatedProject)
    {
        var index = _projects.FindIndex(s => s.Id == id);
        if (index == -1)
            return Task.FromResult<IActionResult>(NotFound());
        _projects[index] = updatedProject;
        return Task.FromResult<IActionResult>(NoContent());
    }

    /// <summary>
    /// Deletes the project with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the project to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that
    /// is <see cref="NotFoundResult"/> if the project does not exist; otherwise, <see cref="NoContentResult"/>.</returns>
    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteProject(long id)
    {
        var project = _projects.FirstOrDefault(s => s.Id == id);
        if (project == null)
            return Task.FromResult<IActionResult>(NotFound());
        _projects.Remove(project);
        return Task.FromResult<IActionResult>(NoContent());
    }
}
