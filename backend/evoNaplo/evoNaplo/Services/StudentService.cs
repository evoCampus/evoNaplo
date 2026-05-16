using evoNaplo.DTO;
using evoNaplo.Models;
using evoNaplo.Exceptions;

namespace evoNaplo.Services;

/// <summary>
/// The StudentService class provides methods for managing students in the application. It allows for retrieving, adding, updating, and deleting students. The service uses an in-memory list to store student data. The service also includes error handling to ensure that appropriate exceptions are thrown when operations fail, such as when a student is not found or when trying to add a student that already exists.
/// </summary>
internal class StudentService : IStudentService
{
    private static readonly List<Student> _students = new List<Student>();
    
    /// <summary>
    /// Retrieves a student model by its ID. If a student with the specified ID is found in the list of students, it is returned. If no student is found with the given ID, a StudentNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the student to retrieve.</param>
    /// <returns>The Student model if found.</returns>
    /// <exception cref="StudentNotFoundException"></exception>
    public Student GetStudentModelById(string id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student is null)
        {
            throw new StudentNotFoundException($"Student with ID {id} not found.");
        }
        return student;
    }

    /// <summary>
    /// Retrieves all students as a collection of StudentDTOs. The method iterates through the list of students and converts each student model into a StudentDTO, which is then returned as an IEnumerable collection. This allows for a more structured and simplified representation of student data when it is accessed by other parts of the application.
    /// </summary>
    /// <returns>An IEnumerable collection of StudentDTOs representing all students.</returns>
    public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
    {
        IEnumerable<StudentDTO> students = _students.Select(s => new StudentDTO(s));
        return students;
    }
    
    /// <summary>
    /// Retrieves a student by its ID and returns it as a StudentDTO. If a student with the specified ID is found in the list of students, it is converted into a StudentDTO and returned. If no student is found with the given ID, a StudentNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the student to retrieve.</param>
    /// <returns>The StudentDTO if found.</returns>
    /// <exception cref="StudentNotFoundException"></exception>
    public async Task<StudentDTO> GetStudentByIdAsync(string id)
    {
        Student? student = _students.FirstOrDefault(s => s.Id == id);
        if (student is not null) 
        {
            return new StudentDTO(student);
        }
        throw new StudentNotFoundException($"Student with ID {id} not found.");
    }

    /// <summary>
    /// Adds a new student to the list of students. The method takes a StudentDTO as input, generates a new unique ID for the student, and creates a new Student model based on the provided DTO. The new student is then added to the list of students, and the original StudentDTO (with the newly assigned ID) is returned. This allows for the creation of new student entries in the application while ensuring that each student has a unique identifier.
    /// </summary>
    /// <param name="studentToAdd">The StudentDTO to add.</param>
    /// <returns>The added StudentDTO if successful.</returns>
    public async Task<StudentDTO> AddStudentAsync(StudentDTO studentToAdd)
    {
        studentToAdd.Id = Guid.NewGuid().ToString();
        _students.Add(new Student
        {
            Id = studentToAdd.Id,
            Name = studentToAdd.Name,
            Email = studentToAdd.Email,
            PhoneNumber = studentToAdd.PhoneNumber,
            UniversityProgramme = studentToAdd.UniversityProgramme,
            CurrentSemester = studentToAdd.CurrentSemester,
            IsFirstEvoCampusSemester = studentToAdd.IsInTheirFirstSemester,
            PersonalGoals = studentToAdd.PersonalGoals,
            HasAppliedForScholarship = studentToAdd.HasAppliedForScholarship,
            HasActiveScholarship = studentToAdd.HasScholarship,
            ScholarshipDuration = studentToAdd.ScholarshipDuration,
            HasAppliedForInternship = studentToAdd.HasAppliedForInternship,
            IsCurrentlyIntern = studentToAdd.HasInternship,
            IsWorkingStudent = studentToAdd.IsWorkingStudent,
            WantsToStayWithCurrentTeam = studentToAdd.WantsToStayWithCurrentTeam,
        });
        return studentToAdd;
    }

    /// <summary>
    /// Updates an existing student with the specified ID using the provided StudentDTO. The method first checks if a student with the given ID exists in the list of students. If a student is found, its properties are updated with the values from the provided StudentDTO, and the updated StudentDTO is returned. If no student is found with the specified ID, a StudentNotFoundException is thrown with an appropriate message. This allows for modifying existing student entries in the application while ensuring that only valid students can be updated.
    /// </summary>
    /// <param name="id">The ID of the student to update.</param>
    /// <param name="updatedStudent">The updated student DTO.</param>
    /// <returns>The updated StudentDTO if successful.</returns>
    /// <exception cref="StudentNotFoundException"></exception>
    public async Task<StudentDTO> UpdateStudentAsync(string id, StudentDTO updatedStudent)
    {
        var existing = _students.FirstOrDefault(s => s.Id == id);
        if (existing is not null) 
        {
            existing.Id = updatedStudent.Id;
            existing.Name = updatedStudent.Name;
            existing.Email = updatedStudent.Email;
            existing.PhoneNumber = updatedStudent.PhoneNumber;
            existing.UniversityProgramme = updatedStudent.UniversityProgramme;
            existing.CurrentSemester = updatedStudent.CurrentSemester;
            existing.IsFirstEvoCampusSemester = updatedStudent.IsInTheirFirstSemester;
            existing.PersonalGoals = updatedStudent.PersonalGoals;
            existing.HasAppliedForScholarship = updatedStudent.HasAppliedForScholarship;
            existing.HasActiveScholarship = updatedStudent.HasScholarship;
            existing.ScholarshipDuration = updatedStudent.ScholarshipDuration;
            existing.HasAppliedForInternship = updatedStudent.HasAppliedForInternship;
            existing.IsCurrentlyIntern = updatedStudent.HasInternship;
            existing.IsWorkingStudent = updatedStudent.IsWorkingStudent;
            existing.WantsToStayWithCurrentTeam = updatedStudent.WantsToStayWithCurrentTeam;
            return updatedStudent;
        }
        throw new StudentNotFoundException($"Student with ID {id} not found.");
    }

    /// <summary>
    /// Deletes a student with the specified ID from the list of students. The method checks if a student with the given ID exists in the list. If a student is found, it is removed from the list, and the method returns true to indicate that the deletion was successful. If no student is found with the specified ID, a StudentNotFoundException is thrown with an appropriate message. This allows for the removal of student entries from the application while ensuring that only valid students can be deleted.
    /// </summary>
    /// <param name="id">The ID of the student to delete.</param>
    /// <returns>A boolean indicating whether the student was deleted.</returns>
    /// <exception cref="StudentNotFoundException"></exception>
    public async Task<bool> DeleteStudentAsync(string id)
    {
        var existing = _students.FirstOrDefault(s => s.Id == id);
        if (existing is not null) 
        {
            _students.Remove(existing);
            return true;
        }
        throw new StudentNotFoundException($"Student with ID {id} not found.");
    }
    
}
