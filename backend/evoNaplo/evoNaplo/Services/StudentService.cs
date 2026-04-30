using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal class StudentService : IStudentService
    {
        private static readonly List<Student> _students = new List<Student>();

        /// <summary>
        /// Gets the student model by their ID. This is used internally for operations that require the full student model, such as updates or deletions.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>The Student model if found, otherwise null.</returns>
        public Student? GetStudentModelById(string id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Gets all students in the system and maps them to StudentDTOs for external use, such as API responses. This method ensures that only the necessary information is exposed while keeping the internal student model encapsulated.
        /// </summary>
        /// <returns>A list of StudentDTOs representing all students in the system.</returns>
        public IEnumerable<StudentDTO> GetAllStudents()
        {
            return _students.Select(student => new StudentDTO 
        { 
            Id = student.Id,
            Name = student.Name ?? "N/A",
            Email = student.Email ?? "N/A",
            PhoneNumber = student.PhoneNumber ?? "N/A",
            UniversityProgramme = student.UniversityProgramme ?? "N/A",
            CurrentSemester = student.CurrentSemester,
            IsInTheirFirstSemester = student.IsFirstEvoCampusSemester,
            PersonalGoals = student.PersonalGoals ?? "N/A",
            HasAppliedForScholarship = student.HasAppliedForScholarship,
            HasScholarship = student.HasActiveScholarship,
            ScholarshipDuration = student.ScholarshipDuration,
            HasAppliedForInternship = student.HasAppliedForInternship,
            HasInternship = student.IsCurrentlyIntern,
            IsWorkingStudent = student.IsWorkingStudent,
            WantsToStayWithCurrentTeam = student.WantsToStayWithCurrentTeam
        });
        }

        /// <summary>
        /// Gets a single student by their ID and maps it to a StudentDTO for external use. This method is useful for retrieving specific student information without exposing the internal model directly.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>The StudentDTO if found, otherwise null.</returns>
        public StudentDTO? GetStudentById(string id)
        {
            Student? student = _students.FirstOrDefault(s => s.Id == id);
            if (student is not null) 
                return new StudentDTO 
                {
                    Id = student.Id,
                    Name = student.Name ?? "N/A",
                    Email = student.Email ?? "N/A",
                    PhoneNumber = student.PhoneNumber ?? "N/A",
                    UniversityProgramme = student.UniversityProgramme ?? "N/A",
                    CurrentSemester = student.CurrentSemester,
                    IsInTheirFirstSemester = student.IsFirstEvoCampusSemester,
                    PersonalGoals = student.PersonalGoals ?? "N/A",
                    HasAppliedForScholarship = student.HasAppliedForScholarship,
                    HasScholarship = student.HasActiveScholarship,
                    ScholarshipDuration = student.ScholarshipDuration,
                    HasAppliedForInternship = student.HasAppliedForInternship,
                    HasInternship = student.IsCurrentlyIntern,
                    IsWorkingStudent = student.IsWorkingStudent,
                    WantsToStayWithCurrentTeam = student.WantsToStayWithCurrentTeam
                };
            else
                return null;
        }

        /// <summary>
        /// Adds a new student to the system based on the provided StudentDTO. This method maps the DTO to the internal Student model and adds it to the in-memory list of students. If the DTO does not contain an ID, a new GUID will be generated for the student.
        /// </summary>
        /// <param name="student">The StudentDTO containing the student's information.</param>
        public void AddStudent(StudentDTO student)
        {
            if (string.IsNullOrEmpty(student.Id)) 
                student.Id = System.Guid.NewGuid().ToString();
            Student newStudent = new Student
            {
                Id = student.Id,
                Name = student.Name ?? "N/A",
                Email = student.Email ?? "N/A",
                PhoneNumber = student.PhoneNumber ?? "N/A",
                UniversityProgramme = student.UniversityProgramme ?? "N/A",
                CurrentSemester = student.CurrentSemester,
                IsFirstEvoCampusSemester = student.IsInTheirFirstSemester,
                PersonalGoals = student.PersonalGoals ?? "N/A",
                HasAppliedForScholarship = student.HasAppliedForScholarship,
                HasActiveScholarship = student.HasScholarship,
                ScholarshipDuration = student.ScholarshipDuration,
                HasAppliedForInternship = student.HasAppliedForInternship,
                IsCurrentlyIntern = student.HasInternship,
                IsWorkingStudent = student.IsWorkingStudent,
                WantsToStayWithCurrentTeam = student.WantsToStayWithCurrentTeam
            };
            _students.Add(newStudent);
        }

        /// <summary>
        /// Updates an existing student's information based on the provided StudentDTO. This method first retrieves the existing student model by ID, then updates its properties with the values from the DTO if they are different and not null. This allows for partial updates where only certain fields need to be changed without affecting others.
        /// </summary>
        /// <param name="id">The ID of the student to update.</param>
        /// <param name="updatedStudent">The updated student DTO.</param>
        public void UpdateStudent(string id, StudentDTO updatedStudent)
        {
            Student? existingStudent = _students.FirstOrDefault(s => s.Id == id);
            if (existingStudent is null || updatedStudent is null) return;

            if (updatedStudent.Name != existingStudent.Name && updatedStudent.Name is not null) 
                existingStudent.Name = updatedStudent.Name ?? "N/A";
            if (updatedStudent.Email != existingStudent.Email && updatedStudent.Email is not null) 
                existingStudent.Email = updatedStudent.Email ?? "N/A";
            if (updatedStudent.PhoneNumber != existingStudent.PhoneNumber && updatedStudent.PhoneNumber is not null) 
                existingStudent.PhoneNumber = updatedStudent.PhoneNumber ?? "N/A";
            if (updatedStudent.UniversityProgramme != existingStudent.UniversityProgramme && updatedStudent.UniversityProgramme is not null) 
                existingStudent.UniversityProgramme = updatedStudent.UniversityProgramme ?? "N/A";
            if (updatedStudent.CurrentSemester != existingStudent.CurrentSemester && updatedStudent.CurrentSemester is not null) 
                existingStudent.CurrentSemester = updatedStudent.CurrentSemester;
            if (updatedStudent.PersonalGoals != existingStudent.PersonalGoals && updatedStudent.PersonalGoals is not null) 
                existingStudent.PersonalGoals = updatedStudent.PersonalGoals;
            if (updatedStudent.IsInTheirFirstSemester != existingStudent.IsFirstEvoCampusSemester) 
                existingStudent.IsFirstEvoCampusSemester = updatedStudent.IsInTheirFirstSemester;
            if (updatedStudent.HasAppliedForScholarship != existingStudent.HasAppliedForScholarship) 
                existingStudent.HasAppliedForScholarship = updatedStudent.HasAppliedForScholarship;
            if (updatedStudent.HasScholarship != existingStudent.HasActiveScholarship) 
                existingStudent.HasActiveScholarship = updatedStudent.HasScholarship;
            if (updatedStudent.ScholarshipDuration != existingStudent.ScholarshipDuration) 
                existingStudent.ScholarshipDuration = updatedStudent.ScholarshipDuration;
            if (updatedStudent.HasAppliedForInternship != existingStudent.HasAppliedForInternship) 
                existingStudent.HasAppliedForInternship = updatedStudent.HasAppliedForInternship;
            if (updatedStudent.HasInternship != existingStudent.IsCurrentlyIntern) 
                existingStudent.IsCurrentlyIntern = updatedStudent.HasInternship;
            if (updatedStudent.IsWorkingStudent != existingStudent.IsWorkingStudent) 
                existingStudent.IsWorkingStudent = updatedStudent.IsWorkingStudent;
            if (updatedStudent.WantsToStayWithCurrentTeam != existingStudent.WantsToStayWithCurrentTeam) 
                existingStudent.WantsToStayWithCurrentTeam = updatedStudent.WantsToStayWithCurrentTeam;
        }

        /// <summary>
        /// Deletes a student from the system based on their ID. This method first retrieves the student model by ID and then removes it from the in-memory list of students if it exists. If the student with the specified ID does not exist, the method simply returns without performing any action.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        public void DeleteStudent(string id)
        {
            var studentToRemove = _students.FirstOrDefault(s => s.Id == id);
            if (studentToRemove != null)
                _students.Remove(studentToRemove);
        }
    }
}
