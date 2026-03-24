using Microsoft.AspNetCore.Mvc;
using evoNaplo.Services.Interface;
using evoNaplo.Services.Models;

namespace evoNaplo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesztProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public TesztProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetAll()
        {
            return Ok(_projectService.GetAllProjects());
        }

        [HttpGet("{id}")]
        public ActionResult<Project> Get(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            _projectService.AddProject(project);
            return CreatedAtAction(nameof(Get), new { id = project.ProjectID }, project);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Project project)
        {
            if (id != project.ProjectID) return BadRequest();
            var existing = _projectService.GetProjectById(id);
            if (existing == null) return NotFound();

            _projectService.UpdateProject(project);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] Project updatedFields)
        {
            var existing = _projectService.GetProjectById(id);
            if (existing == null) return NotFound();

            _projectService.UpdateProjectFields(id, updatedFields);
            return NoContent();
        }

        [HttpPatch("{id}/name")]
        public ActionResult UpdateName(int id, [FromBody] string name)
        {
            var existing = _projectService.GetProjectById(id);
            if (existing == null) return NotFound();

            _projectService.UpdateProjectName(id, name);
            return NoContent();
        }

        [HttpPatch("{id}/description")]
        public ActionResult UpdateDescription(int id, [FromBody] string description)
        {
            var existing = _projectService.GetProjectById(id);
            if (existing == null) return NotFound();

            _projectService.UpdateProjectDescription(id, description);
            return NoContent();
        }

        [HttpPatch("{id}/links")]
        public ActionResult UpdateLinks(int id, [FromBody] string links)
        {
            var existing = _projectService.GetProjectById(id);
            if (existing == null) return NotFound();

            _projectService.UpdateProjectLinks(id, links);
            return NoContent();
        }

        [HttpPatch("{id}/assignedMentors")]
        public ActionResult UpdateAssignedMentors(int id, [FromBody] string mentors)
        {
            var existing = _projectService.GetProjectById(id);
            if (existing == null) return NotFound();

            _projectService.UpdateProjectAssignedMentors(id, mentors);
            return NoContent();
        }

        [HttpPatch("{id}/assignedStudents")]
        public ActionResult UpdateAssignedStudents(int id, [FromBody] string students)
        {
            var existing = _projectService.GetProjectById(id);
            if (existing == null) return NotFound();

            _projectService.UpdateProjectAssignedStudents(id, students);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existing = _projectService.GetProjectById(id);
            if (existing == null) return NotFound();

            _projectService.DeleteProject(id);
            return NoContent();
        }
    }
}
