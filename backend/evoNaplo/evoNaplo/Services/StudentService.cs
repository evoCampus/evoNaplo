namespace evoNaplo.Services;

internal class StudentService : IStudentService
{
    private static readonly List<Student> _students = new List<Student>();
    
    /// <summary>
    /// This method is used internally by the service to retrieve the Student model for operations that require it, such as adding or updating students. It is not intended to be exposed directly to clients, as it returns the internal model rather than a DTO.
    /// </summary>
    /// <param name="id">The ID of the student to retrieve.</param>
    /// <returns>The Student model if found, otherwise null.</returns>
    public Student? GetStudentModelById(string id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    /// <summary>
    /// Retrieves a list of all students in the database. If no students are found, a StudentNotFoundException is thrown with an appropriate message. Each student is returned as a StudentDTO.
    /// </summary>
    /// <returns>A list of StudentDTOs if students are found.</returns>
    /// <exception cref="StudentNotFoundException"></exception>
    public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
    {
        IEnumerable<StudentDTO> students = _students.Select(s => new StudentDTO(s));
        return students;
    }
    
    /// <summary>
    /// Retrieves a specific student by their ID. If a student with the specified ID is found, it is returned as a StudentDTO. If no student is found with the given ID, a StudentNotFoundException is thrown with an appropriate message.
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
    /// Adds a new student to the list. If a student with the same ID already exists, a StudentAlreadyExistsException is thrown with an appropriate message. If the student is added successfully, the provided StudentDTO is returned. If the ID of the student to add is null or empty, a new GUID will be generated and assigned as the ID.
    /// </summary>
    /// <param name="studentToAdd">The StudentDTO to add.</param>
    /// <returns>The added StudentDTO if successful.</returns>
    /// <exception cref="StudentAlreadyExistsException"></exception>
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
    /// Updates an existing student with the specified ID using the provided updated student data. If a student with the given ID is found, it is updated with the new data and the updated StudentDTO is returned. If no student is found with the specified ID, a StudentNotFoundException is thrown with an appropriate message.
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
    /// Deletes a student with the specified ID. If a student with the given ID is found, it is removed from the list of students and the method returns true. If no student is found with the specified ID, a StudentNotFoundException is thrown with an appropriate message and the method returns false.
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
