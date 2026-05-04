using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;

[ApiController]
[Route("api/[controller]")]
internal class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// Retrieves a list of all students in the system. Each student is represented as a <see cref="StudentDTO"/> object containing relevant information about the student. If no students are found, an empty list is returned.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of <see cref="StudentDTO"/> objects representing all students in the system.</returns>
    [HttpGet]
    public Task<IEnumerable<StudentDTO>> GetStudents()
    {
        return Task.FromResult(_studentService.GetAllStudents());
    }

    /// <summary>
    /// Retrieves the details of a specific student based on the provided identifier. If no student with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="StudentDTO"/>
    /// representing the student with the specified identifier.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a student with the specified <paramref name="studentId"/> does not exist.</exception>
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> GetStudent(string studentId)
    {
        StudentDTO? student = _studentService.GetStudentById(studentId);
        if (student is not null)
        {
            return Ok(student);
        }
        else
        {
            return NotFound($"Student with id {studentId} not found.");
        }
    }

    /// <summary>
    /// Creates a new student in the system based on the provided student data. If a student with the same identifier already exists, a Conflict response is returned.
    /// </summary>
    /// <param name="studentToCreate">The student data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created student.</returns>
    [HttpPost]
    public async Task<ActionResult<StudentDTO>> CreateStudent(StudentDTO studentToCreate)
    {
        if (_studentService.GetStudentById(studentToCreate.Id) is null)
        {
            _studentService.AddStudent(studentToCreate);
            return Ok(studentToCreate);
        }
        else
        {
            return Conflict($"Student with ID {studentToCreate.Id} already exists.");
        }
    }

    /// <summary>
    /// Updates the details of an existing student based on the provided identifier and updated student data. If no student with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student to update. Cannot be null or empty.</param>
    /// <param name="updatedStudent">An object containing the updated student information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the student is not found.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStudent(string studentId, StudentDTO updatedStudent)
    {
        if (_studentService.GetStudentById(studentId) is not null)
        {
            _studentService.UpdateStudent(studentId, updatedStudent);
            return NoContent();
        }
        else
        {
            return NotFound($"Student with ID {studentId} not found.");
        }
    }

    /// <summary>
    /// Deletes an existing student from the system based on the provided identifier. If no student with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the deletion is successful; otherwise, an HTTP 404 response if the student is not found.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudent(string studentId)
    {
        if (_studentService.GetStudentById(studentId) is null)
        {
            return NotFound($"Student with ID {studentId} not found.");
        }
        _studentService.DeleteStudent(studentId);
        return NoContent();
    }
}
