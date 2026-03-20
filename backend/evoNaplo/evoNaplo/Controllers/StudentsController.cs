using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static List<StudentDTO> _students = new List<StudentDTO>();
    private static long _nextId = 1;

    /// <summary>
    /// GET: api/students
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<IEnumerable<StudentDTO>> GetStudents()
    {
        return Task.FromResult((IEnumerable<StudentDTO>)_students);
    }

    // GET: api/students/5
    [HttpGet("{id}")]
    public ActionResult<StudentDTO> GetStudent(long id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound();
        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public ActionResult<StudentDTO> CreateStudent(StudentDTO student)
    {
        student.Id = _nextId++;
        _students.Add(student);
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    // PUT: api/students/5
    [HttpPut("{id}")]
    public ActionResult UpdateStudent(long id, StudentDTO updatedStudent)
    {
        var index = _students.FindIndex(s => s.Id == id);
        if (index == -1)
            return NotFound();
        _students[index] = updatedStudent;
        return NoContent();
    }

    // DELETE: api/students/5
    [HttpDelete("{id}")]
    public ActionResult DeleteStudent(long id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) 
            return NotFound();
        _students.Remove(student);
        return NoContent();
    }
}
