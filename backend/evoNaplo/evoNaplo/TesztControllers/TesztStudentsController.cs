using Microsoft.AspNetCore.Mvc;
using evoNaplo.ServiceMappa.Interface;
using evoNaplo.ServiceMappa.TesztDTO;

namespace evoNaplo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesztStudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public TesztStudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("List All")]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            return Ok(_studentService.GetAllStudents());
        }

        [HttpGet("Lis By StudID {id}")]
        public ActionResult<Student> Get(string id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost("Add student")]
        public ActionResult Create(Student student)
        {
            _studentService.AddStudent(student);
            return CreatedAtAction(nameof(Get), new { id = student.StudID }, student);
        }

       

        // Partial update using Student object: only non-null fields will be updated
        [HttpPatch("Update Student{id}")]
        public ActionResult Patch(string id, [FromBody] Student updatedFields)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentFields(id, updatedFields);
            return NoContent();
        }

        // Per-field endpoints
        [HttpPatch("Update {id}/MentorName")]
        public ActionResult UpdateName(string id, [FromBody] string name)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentName(id, name);
            return NoContent();
        }

        [HttpPatch("Update{id}/MentorEmail")]
        public ActionResult UpdateEmail(string id, [FromBody] string email)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentEmail(id, email);
            return NoContent();
        }

        [HttpPatch("Update {id}/phone")]
        public ActionResult UpdatePhone(string id, [FromBody] string phone)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentPhoneNumber(id, phone);
            return NoContent();
        }

        [HttpPatch("U {id}/studies")]
        public ActionResult UpdateStudies(string id, [FromBody] string studies)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentCurrentStudies(id, studies);
            return NoContent();
        }

        [HttpPatch("U {id}/goals")]
        public ActionResult UpdateGoals(string id, [FromBody] string goals)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentPersonalGoals(id, goals);
            return NoContent();
        }

        [HttpPatch("U {id}/firstSemester")]
        public ActionResult UpdateFirstSemester(string id, [FromBody] bool isFirst)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentIsFirstSemester(id, isFirst);
            return NoContent();
        }

        [HttpPatch("U {id}/stayWithTeam")]
        public ActionResult UpdateStayWithTeam(string id, [FromBody] bool stay)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentStayWithCurrentTeam(id, stay);
            return NoContent();
        }

        [HttpPatch("U {id}/assignedTeam")]
        public ActionResult UpdateAssignedTeam(string id, [FromBody] string assignedTeam)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentAssignedTeam(id, assignedTeam);
            return NoContent();
        }

        [HttpPatch("U {id}/assignedProject")]
        public ActionResult UpdateAssignedProject(string id, [FromBody] string assignedProject)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentAssignedProject(id, assignedProject);
            return NoContent();
        }

        [HttpPatch("U {id}/appliedForCampus")]
        public ActionResult UpdateAppliedForCampus(string id, [FromBody] bool applied)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentAppliedForCampus(id, applied);
            return NoContent();
        }

        [HttpPatch("U {id}/activeScholarship")]
        public ActionResult UpdateActiveScholarship(string id, [FromBody] bool active)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentActiveScholarship(id, active);
            return NoContent();
        }

        [HttpPatch("U {id}/scholarshipDuration")]
        public ActionResult UpdateScholarshipDuration(string id, [FromBody] int months)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentScholarshipDuration(id, months);
            return NoContent();
        }

        [HttpPatch("U {id}/internshipApplied")]
        public ActionResult UpdateInternshipApplied(string id, [FromBody] bool applied)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentInternshipApplied(id, applied);
            return NoContent();
        }

        [HttpPatch("U {id}/isAnIntern")]
        public ActionResult UpdateIsAnIntern(string id, [FromBody] bool isAnIntern)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentIsAnIntern(id, isAnIntern);
            return NoContent();
        }

        [HttpPatch("U {id}/doesWork")]
        public ActionResult UpdateDoesWork(string id, [FromBody] bool doesWork)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentDoesWork(id, doesWork);
            return NoContent();
        }

        [HttpPatch("U {id}/workDuration")]
        public ActionResult UpdateWorkDuration(string id, [FromBody] string workDuration)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.UpdateStudentWorkDuration(id, workDuration);
            return NoContent();
        }

        [HttpDelete("Remove {id}")]
        public ActionResult Delete(string id)
        {
            var existing = _studentService.GetStudentById(id);
            if (existing == null) return NotFound();

            _studentService.DeleteStudent(id);
            return NoContent();
        }
    }
}
