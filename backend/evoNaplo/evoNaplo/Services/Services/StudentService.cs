using evoNaplo.Services;
using System.Collections.Generic;
using System.Linq;

namespace evoNaplo.Services.Services
{
    public class StudentService : Interface.IStudentService
    {
    
        private static readonly List<Student> _students = new List<Student>();


      
        public List<Student> GetAllStudents()
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

      




      
        public void UpdateStudentFields(string id, Student updatedFields)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent is null || updatedFields is null)
            {
                return;
            }

            if (updatedFields.StudName != null)
            {
                existingStudent.StudName = updatedFields.StudName;
            }

            if (updatedFields.StudEmail != null)
            {
                existingStudent.StudEmail = updatedFields.StudEmail;
            }

            if (updatedFields.StudPhoneNumber != null)
            {
                existingStudent.StudPhoneNumber = updatedFields.StudPhoneNumber;
            }

            if (updatedFields.CurrentStudies != null)
            {
                existingStudent.CurrentStudies = updatedFields.CurrentStudies;
            }

            if (updatedFields.PersonalGoals != null)
            {
                existingStudent.PersonalGoals = updatedFields.PersonalGoals;
            }

           
            if (updatedFields.ScholarshipDuration != 0)
            {
                existingStudent.ScholarshipDuration = updatedFields.ScholarshipDuration;
            }

           
            if (updatedFields.WorkDuration != null)
            {
                existingStudent.WorkDuration = updatedFields.WorkDuration;
            }

            if (updatedFields.StudAssignedTeam != null)
            {
                existingStudent.StudAssignedTeam = updatedFields.StudAssignedTeam;
            }
            
            if (updatedFields.StudAssignedProject != null)
            {

                existingStudent.StudAssignedProject = updatedFields.StudAssignedProject;
            }

        }
        public void UpdateStudentIsFirstSemester(string id, bool isFirstSemester)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent is null) return;

            existingStudent.IsFirstSemester = isFirstSemester;
        }

        public void UpdateStudentStayWithCurrentTeam(string id, bool stayWithCurrentTeam)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent is null) return;

            existingStudent.StayWithCurrentTeam = stayWithCurrentTeam;
        }

       
        public void UpdateStudentName(string id, string name)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent is null) return;

            existingStudent.StudName = name;
        }

        public void UpdateStudentEmail(string id, string email)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent is null) return;

            existingStudent.StudEmail = email;
        }

        public void UpdateStudentPhoneNumber(string id, string phoneNumber)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StudPhoneNumber = phoneNumber;
        }

        public void UpdateStudentCurrentStudies(string id, string currentStudies)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.CurrentStudies = currentStudies;
        }

        public void UpdateStudentPersonalGoals(string id, string personalGoals)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.PersonalGoals = personalGoals;
        }

        
        public void UpdateStudentAppliedForCampus(string id, bool appliedForCampus)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.AppliedForCampus = appliedForCampus;
        }

        public void UpdateStudentActiveScholarship(string id, bool activeScholarship)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.ActiveScholarship = activeScholarship;
        }

        public void UpdateStudentScholarshipDuration(string id, int scholarshipDuration)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.ScholarshipDuration = scholarshipDuration;
        }

       
       
        public void UpdateStudentInternshipApplied(string id, bool internshipApplied)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.InternshipApplied = internshipApplied;
        }

        public void UpdateStudentIsAnIntern(string id, bool isAnIntern)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.IsAnIntern = isAnIntern;
        }

        public void UpdateStudentDoesWork(string id, bool doesWork)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.doeswork = doesWork;
        }

        public void UpdateStudentWorkDuration(string id, string workDuration)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.WorkDuration = workDuration;
        }

        
        public void UpdateStudentAssignedTeam(string id, string assignedTeam)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StudAssignedTeam = assignedTeam;
        }

        public void UpdateStudentAssignedProject(string id, string assignedProject)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StudAssignedProject = assignedProject;
        }
        
        public void UpdateStudent(string id, Student updatedStudent)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent is null || updatedStudent is null) return;

            existingStudent.StudName = updatedStudent.StudName;
            existingStudent.StudEmail = updatedStudent.StudEmail;
            existingStudent.StudPhoneNumber = updatedStudent.StudPhoneNumber;
            existingStudent.CurrentStudies = updatedStudent.CurrentStudies;
            existingStudent.IsFirstSemester = updatedStudent.IsFirstSemester;
            existingStudent.PersonalGoals = updatedStudent.PersonalGoals;
            existingStudent.AppliedForCampus = updatedStudent.AppliedForCampus;
            existingStudent.ActiveScholarship = updatedStudent.ActiveScholarship;
            existingStudent.ScholarshipDuration = updatedStudent.ScholarshipDuration;
            existingStudent.StayWithCurrentTeam = updatedStudent.StayWithCurrentTeam;
            existingStudent.StudAssignedTeam = updatedStudent.StudAssignedTeam;
            existingStudent.StudAssignedProject = updatedStudent.StudAssignedProject;
            existingStudent.InternshipApplied = updatedStudent.InternshipApplied;
            existingStudent.IsAnIntern = updatedStudent.IsAnIntern;
            existingStudent.doeswork = updatedStudent.doeswork;
            existingStudent.WorkDuration = updatedStudent.WorkDuration;
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
