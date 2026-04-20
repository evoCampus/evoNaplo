using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Models;
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
    /// Retrieves a list of all students in the system, returning their details as StudentDTO objects.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of student data transfer objects.</returns>
    [HttpGet]
    public Task<IEnumerable<StudentDTO>> GetStudents()
    {
        return Task.FromResult(_studentService.GetAllStudents().Select(student => new StudentDTO
        {
            Id = student.Id,
            Name = student.Name ?? "N/A",
            Email = student.Email ?? "N/A",
            PhoneNumber = student.PhoneNumber ?? "N/A",
            UniversityProgramme = student.UniversityProgramme ?? "N/A",
            CurrentSemester = student.CurrentSemester,
            IsInTheirFirstSemester = student.IsInTheirFirstSemester,
            PersonalGoals = student.PersonalGoals ?? "N/A",
            HasAppliedForScholarship = student.HasAppliedForScholarship,
            HasScholarship = student.HasScholarship,
            ScholarshipDurationInSemesters = student.ScholarshipDurationInSemesters,
            HasAppliedForInternship = student.HasAppliedForInternship,
            HasInternship = student.HasInternship,
            IsWorkingStudent = student.IsWorkingStudent,
            WorkExperienceInSemesters = student.WorkExperienceInSemesters,
            WantsToStayWithCurrentTeam = student.WantsToStayWithCurrentTeam
        }));
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
        Student? student = _studentService.GetStudentById(studentId);
        if (student is null)
            return NotFound($"Student with id {studentId} not found.");
        return Ok(new StudentDTO
        {
            Id = student.Id,
            Name = student.Name ?? "N/A",
            Email = student.Email ?? "N/A",
            PhoneNumber = student.PhoneNumber ?? "N/A",
            UniversityProgramme = student.UniversityProgramme ?? "N/A",
            CurrentSemester = student.CurrentSemester,
            IsInTheirFirstSemester = student.IsInTheirFirstSemester,
            PersonalGoals = student.PersonalGoals ?? "N/A",
            HasAppliedForScholarship = student.HasAppliedForScholarship,
            HasScholarship = student.HasScholarship,
            ScholarshipDurationInSemesters = student.ScholarshipDurationInSemesters,
            HasAppliedForInternship = student.HasAppliedForInternship,
            HasInternship = student.HasInternship,
            IsWorkingStudent = student.IsWorkingStudent,
            WorkExperienceInSemesters = student.WorkExperienceInSemesters,
            WantsToStayWithCurrentTeam = student.WantsToStayWithCurrentTeam
        });
    }

    /// <summary>
    /// Creates a new student in the system based on the provided student data. If a student with the same identifier already exists, a Conflict response is returned.
    /// </summary>
    /// <param name="studentToCreate">The student data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created student.</returns>
    /// <exception cref="ConflictException">Thrown if a student with the specified ID already exists.</exception>
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(StudentDTO studentToCreate)
    {
        if (_studentService.GetStudentById(studentToCreate.Id) is not null)
            return Conflict($"Student with ID {studentToCreate.Id} already exists.");
        Student newStudent = new Student
        {
            Id = studentToCreate.Id,
            Name = studentToCreate.Name ?? "N/A",
            Email = studentToCreate.Email ?? "N/A",
            PhoneNumber = studentToCreate.PhoneNumber ?? "N/A",
            UniversityProgramme = studentToCreate.UniversityProgramme ?? "N/A",
            CurrentSemester = studentToCreate.CurrentSemester,
            IsInTheirFirstSemester = studentToCreate.IsInTheirFirstSemester,
            PersonalGoals = studentToCreate.PersonalGoals ?? "N/A",
            HasAppliedForScholarship = studentToCreate.HasAppliedForScholarship,
            HasScholarship = studentToCreate.HasScholarship,
            ScholarshipDurationInSemesters = studentToCreate.ScholarshipDurationInSemesters,
            HasAppliedForInternship = studentToCreate.HasAppliedForInternship,
            HasInternship = studentToCreate.HasInternship,
            IsWorkingStudent = studentToCreate.IsWorkingStudent,
            WorkExperienceInSemesters = studentToCreate.WorkExperienceInSemesters,
            WantsToStayWithCurrentTeam = studentToCreate.WantsToStayWithCurrentTeam
        };
        _studentService.AddStudent(newStudent);
        return Ok(newStudent);
    }

    /// <summary>
    /// Updates the details of an existing student in the system based on the provided identifier and updated student data. If no student with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student to update. Cannot be null or empty.</param>
    /// <param name="updatedStudent">An object containing the updated student information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the student is not found.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStudent(string studentId, StudentDTO updatedStudent)
    {
        if (_studentService.GetStudentById(studentId) is null)
            return NotFound($"Student with ID {studentId} not found.");
        Student student = new Student
        {
            Id = updatedStudent.Id,
            Name = updatedStudent.Name ?? "N/A",
            Email = updatedStudent.Email ?? "N/A",
            PhoneNumber = updatedStudent.PhoneNumber ?? "N/A",
            UniversityProgramme = updatedStudent.UniversityProgramme ?? "N/A",
            CurrentSemester = updatedStudent.CurrentSemester,
            IsInTheirFirstSemester = updatedStudent.IsInTheirFirstSemester,
            PersonalGoals = updatedStudent.PersonalGoals ?? "N/A",
            HasAppliedForScholarship = updatedStudent.HasAppliedForScholarship,
            HasScholarship = updatedStudent.HasScholarship,
            ScholarshipDurationInSemesters = updatedStudent.ScholarshipDurationInSemesters,
            HasAppliedForInternship = updatedStudent.HasAppliedForInternship,
            HasInternship = updatedStudent.HasInternship,
            IsWorkingStudent = updatedStudent.IsWorkingStudent,
            WorkExperienceInSemesters = updatedStudent.WorkExperienceInSemesters,
            WantsToStayWithCurrentTeam = updatedStudent.WantsToStayWithCurrentTeam
        };
        _studentService.UpdateStudent(studentId, student);
        return NoContent();
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
            return NotFound($"Student with ID {studentId} not found.");
        _studentService.DeleteStudent(studentId);
        return NoContent();
    }
}
