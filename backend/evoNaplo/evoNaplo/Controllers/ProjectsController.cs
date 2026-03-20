using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private static List<ProjectDTO> _projects = new List<ProjectDTO>();
    private static long _nextId = 1;

    // GET: api/projects
    [HttpGet]
    public ActionResult<IEnumerable<ProjectDTO>> GetProjects()
    {
        return Ok(_projects);
    }

    // GET: api/projects/5
    [HttpGet("{id}")]
    public ActionResult<ProjectDTO> GetProject(long id)
    {
        var project = _projects.FirstOrDefault(s => s.Id == id);
        if (project == null)
            return NotFound();
        return Ok(project);
    }

    // POST: api/projects
    [HttpPost]
    public ActionResult<ProjectDTO> CreateProject(ProjectDTO project)
    {
        project.Id = _nextId++;
        _projects.Add(project);
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    // PUT: api/projects/5
    [HttpPut("{id}")]
    public ActionResult UpdateProject(long id, ProjectDTO updatedProject)
    {
        var index = _projects.FindIndex(s => s.Id == id);
        if (index == -1)
            return NotFound();
        _projects[index] = updatedProject;
        return NoContent();
    }

    // DELETE: api/projects/5
    [HttpDelete("{id}")]
    public ActionResult DeleteProject(long id)
    {
        var project = _projects.FirstOrDefault(s => s.Id == id);
        if (project == null)
            return NotFound();
        _projects.Remove(project);
        return NoContent();
    }
}
