namespace evoNaplo.Controllers;

/// <summary>
/// Controller for managing students in the application. Provides endpoints for retrieving, creating, updating, and deleting student records. Each endpoint interacts with the IStudentService to perform the necessary operations on the student data. The controller handles exceptions such as StudentNotFoundException and StudentAlreadyExistsException to return appropriate HTTP responses based on the outcome of each operation.
/// </summary>
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
    /// Retrieves a list of all students. If students are found, they are returned as a list of StudentDTO objects. If no students are found, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <returns>A list of StudentDTO objects if students are found; otherwise, a NotFound response.</returns>
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
    /// Retrieves a specific student by their unique identifier. If a student with the given identifier exists, it is returned as a StudentDTO object. If no student is found with the provided identifier, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student to retrieve.</param>
    /// <returns>The StudentDTO if found; otherwise, a NotFound response.</returns>
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
    /// Creates a new student in the database based on the provided StudentDTO object. If a student with the same unique identifier already exists, a Conflict response is returned with an appropriate message. If the student is created successfully, the newly created StudentDTO object is returned in the response.
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
    /// Updates an existing student in the database based on the provided identifier and updated StudentDTO object. If no student with the given identifier exists, a NotFound response is returned with an appropriate message. If the student is updated successfully, the updated StudentDTO object is returned in the response.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student to update. Cannot be null or empty.</param>
    /// <param name="updatedStudent">An object containing the updated student information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated student.</returns>
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
    /// Deletes a student from the database based on the provided identifier. If no student with the given identifier exists, a NotFound response is returned with an appropriate message. If the student is deleted successfully, an Ok response is returned indicating the successful deletion.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 200 response if the deletion is successful; otherwise, an HTTP 404 response if the student is not found.</returns>
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
