using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;
using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal class StudentService : IStudentService
    {
        private static readonly List<Student> _students = new List<Student>();

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

        public void DeleteStudent(string id)
        {
            
            var studentToRemove = _students.FirstOrDefault(s => s.Id == id);

            
            if (studentToRemove != null)
            {
                _students.Remove(studentToRemove);
            }
        }
    }
}
