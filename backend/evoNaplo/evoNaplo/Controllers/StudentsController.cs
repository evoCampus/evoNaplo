using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static List<StudentDTO> _students = new List<StudentDTO>();

    /// <summary>
    /// Retrieves a collection of all students.
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
    /// <param name="id">The unique identifier of the student to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="StudentDTO"/>
    /// representing the student with the specified identifier.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a student with the specified <paramref name="id"/> does not exist.</exception>
    [HttpGet("{id}")]
    public Task<StudentDTO> GetStudent(string id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student is null)
            throw new KeyNotFoundException($"Student with id {id} not found.");
        return Task.FromResult<StudentDTO>(student);
    }

    /// <summary>
    /// Creates a new student record and returns the created student.
    /// </summary>
    /// <param name="student">The student information to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created student.</returns>
    [HttpPost]
    public Task<StudentDTO> CreateStudent(StudentDTO student)
    {
        _students.Add(student);
        return Task.FromResult<StudentDTO>(student);
    }

    /// <summary>
    /// Updates the details of an existing student with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student to update. Cannot be null or empty.</param>
    /// <param name="updatedStudent">An object containing the updated student information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. Returns a 204 No Content response if the update is
    /// successful; returns a 404 Not Found response if a student with the specified identifier does not exist.</returns>
    [HttpPut("{id}")]
    public Task UpdateStudent(string id, StudentDTO updatedStudent)
    {
        var index = _students.FindIndex(s => s.Id == id);
        if (index == -1)
            return Task.FromResult(NotFound());
        _students[index] = updatedStudent;
        return Task.FromResult(NoContent());
    }

    /// <summary>
    /// Deletes the student with the specified identifier from the collection.
    /// </summary>
    /// <remarks>If the specified student does not exist, the operation completes without error and no action
    /// is taken.</remarks>
    /// <param name="id">The unique identifier of the student to delete. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    [HttpDelete("{id}")]
    public Task DeleteStudent(string id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        _students.Remove(student);
        return Task.FromResult(NoContent());
    }
}
