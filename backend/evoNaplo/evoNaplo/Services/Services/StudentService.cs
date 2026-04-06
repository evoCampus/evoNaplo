using evoNaplo.Services;
using System.Collections.Generic;
using System.Linq;

namespace evoNaplo.Services.Services
{
    internal class StudentService : Interface.IStudentService
    {
    
        private static readonly List<Student> _students = new List<Student>();


      
        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

       
        public Student GetStudentById(string id)
        {
            return _students.FirstOrDefault(s => s.StudID == id);
        }

       
        public void AddStudent(Student student)
        {
            if (string.IsNullOrEmpty(student.StudID)) student.StudID = System.Guid.NewGuid().ToString();
            _students.Add(student);
        }
      
        public void UpdateStudent(string id, Student updatedStudent)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent is null || updatedStudent is null) return;

            if (updatedStudent.StudName is not null) existingStudent.StudName = updatedStudent.StudName;
            if (updatedStudent.StudEmail is not null) existingStudent.StudEmail = updatedStudent.StudEmail;
            if (updatedStudent.StudPhoneNumber is not null) existingStudent.StudPhoneNumber = updatedStudent.StudPhoneNumber;
            if (updatedStudent.CurrentStudies is not null) existingStudent.CurrentStudies = updatedStudent.CurrentStudies;
            
            if (updatedStudent.IsFirstSemester != existingStudent.IsFirstSemester) existingStudent.IsFirstSemester = updatedStudent.IsFirstSemester;
            if (updatedStudent.PersonalGoals != null) existingStudent.PersonalGoals = updatedStudent.PersonalGoals;
            if (updatedStudent.AppliedForCampus != existingStudent.AppliedForCampus) existingStudent.AppliedForCampus = updatedStudent.AppliedForCampus;
            if (updatedStudent.ActiveScholarship != existingStudent.ActiveScholarship) existingStudent.ActiveScholarship = updatedStudent.ActiveScholarship;
            if (updatedStudent.ScholarshipDuration != 0) existingStudent.ScholarshipDuration = updatedStudent.ScholarshipDuration;
            if (updatedStudent.StayWithCurrentTeam != existingStudent.StayWithCurrentTeam) existingStudent.StayWithCurrentTeam = updatedStudent.StayWithCurrentTeam;
            if (updatedStudent.StudAssignedTeam != null) existingStudent.StudAssignedTeam = updatedStudent.StudAssignedTeam;
            if (updatedStudent.StudAssignedProject != null) existingStudent.StudAssignedProject = updatedStudent.StudAssignedProject;
            if (updatedStudent.InternshipApplied != existingStudent.InternshipApplied) existingStudent.InternshipApplied = updatedStudent.InternshipApplied;
            if (updatedStudent.IsAnIntern != existingStudent.IsAnIntern) existingStudent.IsAnIntern = updatedStudent.IsAnIntern;
            if (updatedStudent.doeswork != existingStudent.doeswork) existingStudent.doeswork = updatedStudent.doeswork;
            if (updatedStudent.WorkDuration != null) existingStudent.WorkDuration = updatedStudent.WorkDuration;
        }

        
        public void DeleteStudent(string id)
        {
            
            var studentToRemove = _students.FirstOrDefault(s => s.StudID == id);

            
            if (studentToRemove != null)
            {
                _students.Remove(studentToRemove);
            }
        }
    }
}
