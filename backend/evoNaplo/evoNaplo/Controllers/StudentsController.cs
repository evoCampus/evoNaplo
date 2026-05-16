namespace evoNaplo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// Retrieves a list of all students in the database. If no students are found, a NotFound response is returned with an appropriate message. Each student is returned as a StudentDTO.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="StudentDTO"/> objects representing all students in the database.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
    {
        try
        {
            return Ok(await _studentService.GetAllStudentsAsync());
        }
        catch (StudentNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a specific student by their ID. If a student with the specified ID is found, it is returned as a StudentDTO. If no student is found with the given ID, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <param name="studentId">The ID of the student to retrieve.</param>
    /// <returns>The StudentDTO if found, otherwise a NotFound response.</returns>
    [HttpGet("{studentId}")]
    public async Task<ActionResult<StudentDTO>> GetStudent(string studentId)
    {
        try
        {
            return Ok(await _studentService.GetStudentByIdAsync(studentId));
        }
        catch (StudentNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Adds a new student to the database. If a student with the same ID already exists, a Conflict response is returned with an appropriate message. If the student is added successfully, the created StudentDTO is returned in the response body.
    /// </summary>
    /// <param name="studentToCreate">The student data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created student.</returns>
    [HttpPost]
    public async Task<ActionResult<StudentDTO>> CreateStudent(StudentDTO studentToCreate)
    {
        try
        {
            return Ok(await _studentService.AddStudentAsync(studentToCreate));
        }
        catch (StudentAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing student in the database based on the provided identifier and the provided updated student data. If no student with the given identifier exists, a NotFound response is returned with an appropriate message. If the student is updated successfully, a NoContent response is returned.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student to update. Cannot be null or empty.</param>
    /// <param name="updatedStudent">An object containing the updated student information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the student is not found.</returns>
    [HttpPut("{studentId}")]
    public async Task<ActionResult> UpdateStudent(string studentId, StudentDTO updatedStudent)
    {
        try
        {
            return Ok(await _studentService.UpdateStudentAsync(studentId, updatedStudent));
        }
        catch (StudentNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a student from the database based on the provided identifier. If no student with the given identifier exists, a NotFound response is returned with an appropriate message. If the student is deleted successfully, a NoContent response is returned.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the deletion is successful; otherwise, an HTTP 404 response if the student is not found.</returns>
    [HttpDelete("{studentId}")]
    public async Task<ActionResult> DeleteStudent(string studentId)
    {
        try
        {
            return Ok(await _studentService.DeleteStudentAsync(studentId));
        }
        catch (StudentNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
}
