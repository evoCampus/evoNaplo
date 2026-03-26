using Microsoft.AspNetCore.Mvc;
using evoNaplo.ServiceMappa.Interface;
using evoNaplo.ServiceMappa.TesztDTO;

namespace evoNaplo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesztTeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TesztTeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Team>> GetAll()
        {
            return Ok(_teamService.GetAllTeams());
        }

        [HttpGet("{id}")]
        public ActionResult<Team> Get(string id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null) return NotFound();
            return Ok(team);
        }

        [HttpPost]
        public ActionResult Create(Team team)
        {
            _teamService.AddTeam(team);
            return CreatedAtAction(nameof(Get), new { id = team.TeamID }, team);
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, Team team)
        {
            if (id != team.TeamID) return BadRequest();
            var existing = _teamService.GetTeamById(id);
            if (existing == null) return NotFound();

            _teamService.UpdateTeam(team);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(string id, [FromBody] Team updatedFields)
        {
            var existing = _teamService.GetTeamById(id);
            if (existing == null) return NotFound();

            _teamService.UpdateTeamFields(id, updatedFields);
            return NoContent();
        }

        [HttpPatch("{id}/assignedMentors")]
        public ActionResult UpdateAssignedMentors(string id, [FromBody] string mentors)
        {
            var existing = _teamService.GetTeamById(id);
            if (existing == null) return NotFound();

            _teamService.UpdateTeamAssignedMentors(id, mentors);
            return NoContent();
        }

        [HttpPatch("{id}/assignedStudents")]
        public ActionResult UpdateAssignedStudents(string id, [FromBody] string students)
        {
            var existing = _teamService.GetTeamById(id);
            if (existing == null) return NotFound();

            _teamService.UpdateTeamAssignedStudents(id, students);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existing = _teamService.GetTeamById(id);
            if (existing == null) return NotFound();

            _teamService.DeleteTeam(id);
            return NoContent();
        }
    }
}
