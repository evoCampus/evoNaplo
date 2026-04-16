using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;
using evoNaplo.Models;
namespace evoNaplo.Services
{
    internal class StudentService : IStudentService
    {
    
        private static readonly List<Student> _students = new List<Student>();


      
        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

       
        public Student GetStudentById(string id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

       
        public void AddStudent(Student student)
        {
            if (string.IsNullOrEmpty(student.Id)) student.Id = System.Guid.NewGuid().ToString();
            _students.Add(student);
        }
      
        public void UpdateStudent(string id, Student updatedStudent)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == id);
            if (existingStudent is null || updatedStudent is null) return;

            if (updatedStudent.Name is not null) existingStudent.Name = updatedStudent.Name;
            if (updatedStudent.Email is not null) existingStudent.Email = updatedStudent.Email;
            if (updatedStudent.PhoneNumber is not null) existingStudent.PhoneNumber = updatedStudent.PhoneNumber;
            if (updatedStudent.UniversityName is not null) existingStudent.UniversityName = updatedStudent.UniversityName;
            if (updatedStudent.UniversityProgramme is not null) existingStudent.UniversityProgramme = updatedStudent.UniversityProgramme;
            if (updatedStudent.CurrentSemester != existingStudent.CurrentSemester) existingStudent.CurrentSemester = updatedStudent.CurrentSemester;
            if (updatedStudent.PersonalGoals != null) existingStudent.PersonalGoals = updatedStudent.PersonalGoals;
            if (updatedStudent.IsFirstEvoCampusSemester != existingStudent.IsFirstEvoCampusSemester) existingStudent.IsFirstEvoCampusSemester = updatedStudent.IsFirstEvoCampusSemester;
            if (updatedStudent.HasAppliedForScholarship != existingStudent.HasAppliedForScholarship) existingStudent.HasAppliedForScholarship = updatedStudent.HasAppliedForScholarship;
            if (updatedStudent.HasActiveScholarship != existingStudent.HasActiveScholarship) existingStudent.HasActiveScholarship = updatedStudent.HasActiveScholarship;
            if (updatedStudent.ScholarshipDuration != existingStudent.ScholarshipDuration) existingStudent.ScholarshipDuration = updatedStudent.ScholarshipDuration;
            if (updatedStudent.HasAppliedForInternship != existingStudent.HasAppliedForInternship) existingStudent.HasAppliedForInternship = updatedStudent.HasAppliedForInternship;
            if (updatedStudent.IsCurrentlyIntern != existingStudent.IsCurrentlyIntern) existingStudent.IsCurrentlyIntern = updatedStudent.IsCurrentlyIntern;
            if (updatedStudent.IsWorkingStudent != existingStudent.IsWorkingStudent) existingStudent.IsWorkingStudent = updatedStudent.IsWorkingStudent;
            if (updatedStudent.WorkingStudentDuration != null) existingStudent.WorkingStudentDuration = updatedStudent.WorkingStudentDuration;
            if (updatedStudent.Team is not null) existingStudent.Team = updatedStudent.Team;
            if (updatedStudent.WantsToStayWithCurrentTeam != existingStudent.WantsToStayWithCurrentTeam) existingStudent.WantsToStayWithCurrentTeam = updatedStudent.WantsToStayWithCurrentTeam;
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
