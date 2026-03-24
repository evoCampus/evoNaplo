using Microsoft.AspNetCore.Mvc;
using evoNaplo.Services.Interface;
using evoNaplo.Services.Models;

namespace evoNaplo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesztMentorController : ControllerBase
    {
        private readonly IMentorService _mentorService;

        public TesztMentorController(IMentorService mentorService)
        {
            _mentorService = mentorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Mentor>> GetAll()
        {
            return Ok(_mentorService.GetAllMentors());
        }

        [HttpGet("{id}")]
        public ActionResult<Mentor> Get(int id)
        {
            var mentor = _mentorService.GetMentorById(id);
            if (mentor == null) return NotFound();
            return Ok(mentor);
        }

        [HttpPost]
        public ActionResult Create(Mentor mentor)
        {
            _mentorService.AddMentor(mentor);
            return CreatedAtAction(nameof(Get), new { id = mentor.MentorId }, mentor);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Mentor mentor)
        {
            if (id != mentor.MentorId) return BadRequest();
            var existing = _mentorService.GetMentorById(id);
            if (existing == null) return NotFound();

            _mentorService.UpdateMentor(mentor);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] Mentor updatedFields)
        {
            var existing = _mentorService.GetMentorById(id);
            if (existing == null) return NotFound();

            _mentorService.UpdateMentorFields(id, updatedFields);
            return NoContent();
        }

        [HttpPatch("{id}/name")]
        public ActionResult UpdateName(int id, [FromBody] string name)
        {
            var existing = _mentorService.GetMentorById(id);
            if (existing == null) return NotFound();

            _mentorService.UpdateMentorName(id, name);
            return NoContent();
        }

        [HttpPatch("{id}/email")]
        public ActionResult UpdateEmail(int id, [FromBody] string email)
        {
            var existing = _mentorService.GetMentorById(id);
            if (existing == null) return NotFound();

            _mentorService.UpdateMentorEmail(id, email);
            return NoContent();
        }

        [HttpPatch("{id}/phone")]
        public ActionResult UpdatePhone(int id, [FromBody] string phone)
        {
            var existing = _mentorService.GetMentorById(id);
            if (existing == null) return NotFound();

            _mentorService.UpdateMentorPhone(id, phone);
            return NoContent();
        }

        [HttpPatch("{id}/assignedTeam")]
        public ActionResult UpdateAssignedTeam(int id, [FromBody] string team)
        {
            var existing = _mentorService.GetMentorById(id);
            if (existing == null) return NotFound();

            _mentorService.UpdateMentorAssignedTeam(id, team);
            return NoContent();
        }

        [HttpPatch("{id}/assignedProject")]
        public ActionResult UpdateAssignedProject(int id, [FromBody] string project)
        {
            var existing = _mentorService.GetMentorById(id);
            if (existing == null) return NotFound();

            _mentorService.UpdateMentorAssignedProject(id, project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existing = _mentorService.GetMentorById(id);
            if (existing == null) return NotFound();

            _mentorService.DeleteMentor(id);
            return NoContent();
        }
    }
}
