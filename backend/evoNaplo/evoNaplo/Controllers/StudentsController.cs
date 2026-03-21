using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static List<StudentDTO> _students = new List<StudentDTO>();
    private static long _nextId = 1;

    /// <summary>
    /// Retrieves a collection of students as data transfer objects.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of student
    /// data transfer objects. The collection is empty if no students are available.</returns>
    [HttpGet]
    public Task<IEnumerable<StudentDTO>> GetStudents()
    {
        return Task.FromResult((IEnumerable<StudentDTO>)_students);
    }

    /// <summary>
    /// Retrieves the student with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ActionResult{StudentDTO}"/> with the student data if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{id}")]
    public Task<ActionResult<StudentDTO>> GetStudent(long id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return Task.FromResult<ActionResult<StudentDTO>>(NotFound());
        return Task.FromResult<ActionResult<StudentDTO>>(Ok(student));
    }

    /// <summary>
    /// Creates a new student record and returns the created student information.
    /// </summary>
    /// <param name="student">The student data to create. The student identifier is assigned by the server and any value provided will be
    /// ignored.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ActionResult with the created
    /// student data and a location header pointing to the new resource.</returns>
    [HttpPost]
    public Task<ActionResult<StudentDTO>> CreateStudent(StudentDTO student)
    {
        student.Id = _nextId++;
        _students.Add(student);
        return Task.FromResult<ActionResult<StudentDTO>>(CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student));
    }

    /// <summary>
    /// Updates the details of an existing student with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student to update.</param>
    /// <param name="updatedStudent">An object containing the updated student information. All required fields must be provided.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/>
    /// indicating the outcome: <see cref="NoContentResult"/> if the update is successful; <see cref="NotFoundResult"/>
    /// if a student with the specified identifier does not exist.</returns>
    [HttpPut("{id}")]
    public Task<IActionResult> UpdateStudent(long id, StudentDTO updatedStudent)
    {
        var index = _students.FindIndex(s => s.Id == id);
        if (index == -1)
            return Task.FromResult<IActionResult>(NotFound());
        _students[index] = updatedStudent;
        return Task.FromResult<IActionResult>(NoContent());
    }

    /// <summary>
    /// Deletes the student with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that
    /// is <see cref="NotFoundResult"/> if the student does not exist; otherwise, <see cref="NoContentResult"/>.</returns>
    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteStudent(long id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) 
            return Task.FromResult<IActionResult>(NotFound());
        _students.Remove(student);
        return Task.FromResult<IActionResult>(NoContent());
    }
}
