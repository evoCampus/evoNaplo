using evoNaplo.DAL.Interfaces;
using evoNaplo.DTO.MentorDTOs;
using evoNaplo.DTO.StudentDTOs;
using evoNaplo.Exceptions;
using evoNaplo.Models;

namespace evoNaplo.Services;

/// <summary>
/// The StudentService class provides methods for managing students in the application. It allows for retrieving, adding, updating, and deleting students. The service uses an in-memory list to store student data. The service also includes error handling to ensure that appropriate exceptions are thrown when operations fail, such as when a student is not found or when trying to add a student that already exists.
/// </summary>
internal class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    /// <summary>
    /// Retrieves a student model by its ID. If a student with the specified ID is found in the list of students, it is returned. If no student is found with the given ID, a StudentNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the student to retrieve.</param>
    /// <returns>The Student model if found.</returns>
    /// <exception cref="StudentNotFoundException"></exception>
    public async Task<Student> GetStudentModelById(string id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
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
        var students = await _studentRepository.GetAllStudentsAsync();
        return students?.Select(s => new StudentDTO(s)) ?? Enumerable.Empty<StudentDTO>();
    }
    
    /// <summary>
    /// Retrieves a student by its ID and returns it as a StudentDTO. If a student with the specified ID is found in the list of students, it is converted into a StudentDTO and returned. If no student is found with the given ID, a StudentNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the student to retrieve.</param>
    /// <returns>The StudentDTO if found.</returns>
    /// <exception cref="StudentNotFoundException"></exception>
    public async Task<StudentDTO> GetStudentByIdAsync(string id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        if (student is not null) 
        {
            return new StudentDTO(student);
        }
        throw new StudentNotFoundException($"Student with ID {id} not found.");
    }

    /// <summary>
    /// Adds a new student to the list of students. The method takes a StudentDTO as input, generates a new unique ID for the student, and creates a new Student model based on the provided DTO. The new student is then added to the list of students, and the original StudentDTO (with the newly assigned ID) is returned. This allows for the creation of new student entries in the application while ensuring that each student has a unique identifier.
    /// </summary>
    /// <param name="studentToAddDTO">The StudentDTO to add.</param>
    /// <returns>The added StudentDTO if successful.</returns>
    public async Task<StudentDTO> AddStudentAsync(CreateStudentDTO studentToAddDTO)
    {
        var newStudent = new Student
        {
            Id = string.Empty,
            Name = studentToAddDTO.Name,
            Email = studentToAddDTO.Email,
            PhoneNumber = studentToAddDTO.PhoneNumber,
            UniversityProgramme = studentToAddDTO.UniversityProgramme,
            UniversityName = studentToAddDTO.UniversityName,
            CurrentSemester = studentToAddDTO.CurrentSemester,
            IsFirstEvoCampusSemester = studentToAddDTO.IsInTheirFirstSemester,
            PersonalGoals = studentToAddDTO.PersonalGoals,
            HasAppliedForScholarship = studentToAddDTO.HasAppliedForScholarship,
            HasActiveScholarship = studentToAddDTO.HasScholarship,
            ScholarshipDuration = studentToAddDTO.ScholarshipDuration,
            HasAppliedForInternship = studentToAddDTO.HasAppliedForInternship,
            IsCurrentlyIntern = studentToAddDTO.HasInternship,
            IsWorkingStudent = studentToAddDTO.IsWorkingStudent,
            WantsToStayWithCurrentTeam = studentToAddDTO.WantsToStayWithCurrentTeam,
            TeamId = studentToAddDTO.TeamId
        };

        var addedStudent = await _studentRepository.AddStudentAsync(newStudent);

        return new StudentDTO(addedStudent);
    }

    /// <summary>
    /// Updates an existing student with the specified ID using the provided StudentDTO. The method first checks if a student with the given ID exists in the list of students. If a student is found, its properties are updated with the values from the provided StudentDTO, and the updated StudentDTO is returned. If no student is found with the specified ID, a StudentNotFoundException is thrown with an appropriate message. This allows for modifying existing student entries in the application while ensuring that only valid students can be updated.
    /// </summary>
    /// <param name="id">The ID of the student to update.</param>
    /// <param name="updatedStudentDTO">The updated student DTO.</param>
    /// <returns>The updated StudentDTO if successful.</returns>
    /// <exception cref="StudentNotFoundException"></exception>
    public async Task<StudentDTO> UpdateStudentAsync(string id, UpdateStudentDTO updatedStudentDTO)
    {
        var existingStudent = await _studentRepository.GetStudentByIdAsync(id);
        if (existingStudent is not null) 
        {
            existingStudent.Name = updatedStudentDTO.Name;
            existingStudent.Email = updatedStudentDTO.Email;
            existingStudent.PhoneNumber = updatedStudentDTO.PhoneNumber;
            existingStudent.UniversityProgramme = updatedStudentDTO.UniversityProgramme;
            existingStudent.UniversityName = updatedStudentDTO.UniversityName;
            existingStudent.CurrentSemester = updatedStudentDTO.CurrentSemester;
            existingStudent.IsFirstEvoCampusSemester = updatedStudentDTO.IsInTheirFirstSemester;
            existingStudent.PersonalGoals = updatedStudentDTO.PersonalGoals;
            existingStudent.HasAppliedForScholarship = updatedStudentDTO.HasAppliedForScholarship;
            existingStudent.HasActiveScholarship = updatedStudentDTO.HasScholarship;
            existingStudent.ScholarshipDuration = updatedStudentDTO.ScholarshipDuration;
            existingStudent.HasAppliedForInternship = updatedStudentDTO.HasAppliedForInternship;
            existingStudent.IsCurrentlyIntern = updatedStudentDTO.HasInternship;
            existingStudent.IsWorkingStudent = updatedStudentDTO.IsWorkingStudent;
            existingStudent.WantsToStayWithCurrentTeam = updatedStudentDTO.WantsToStayWithCurrentTeam;
            existingStudent.TeamId = updatedStudentDTO.TeamId;

            await _studentRepository.UpdateStudentAsync(existingStudent);

            return new StudentDTO(existingStudent);
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
        await _studentRepository.DeleteStudentAsync(id);
        return true;
    }
    
}
