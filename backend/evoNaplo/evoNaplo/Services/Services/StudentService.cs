using evoNaplo.Services;
using System.Collections.Generic;
using System.Linq;

namespace evoNaplo.Services.Services
{
    public class StudentService : Interface.IStudentService
    {
        // STATIKUS adattagok: Ezek a memóriában maradnak a program leállásáig.
        private static readonly List<Student> _students = new List<Student>();


        // Visszaadja a listában lévő összes entitást
        public List<Student> GetAllStudents()
        {
            return _students;
        }

        // Keresés ID alapján. Ha nem találja, null értékkel tér vissza.
        public Student GetStudentById(string id)
        {
            return _students.FirstOrDefault(s => s.StudID == id);
        }

        // Hozzáadás a listához
        public void AddStudent(Student student)
        {
            if (string.IsNullOrEmpty(student.StudID)) student.StudID = System.Guid.NewGuid().ToString();
            _students.Add(student);
        }

        // Módosítás




        // Részleges frissítés: csak a megadott (nem null) mezőket frissítjük
        public void UpdateStudentFields(string id, Student updatedFields)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null || updatedFields == null)
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

            // evoCampus fields
            if (updatedFields.ScholarshipDuration != 0)
            {
                existingStudent.ScholarshipDuration = updatedFields.ScholarshipDuration;
            }

            // Work duration
            if (updatedFields.WorkDuration != null)
            {
                existingStudent.WorkDuration = updatedFields.WorkDuration;
            }

            if (updatedFields.StudAssignedTeam != null)
            {
                existingStudent.StudAssignedTeam = updatedFields.StudAssignedTeam;
            }
            // Boolean mezők esetén külön metódusokat használunk, mivel a bool nem nullable a modellben

            if (updatedFields.StudAssignedProject != null)
            {

                existingStudent.StudAssignedProject = updatedFields.StudAssignedProject;
            }

        }

        // Külön metódusok a boolean mezők frissítésére
        public void UpdateStudentIsFirstSemester(string id, bool isFirstSemester)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.IsFirstSemester = isFirstSemester;
        }

        public void UpdateStudentStayWithCurrentTeam(string id, bool stayWithCurrentTeam)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StayWithCurrentTeam = stayWithCurrentTeam;
        }

        // Single-field update methods
        public void UpdateStudentName(string id, string name)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StudName = name;
        }

        public void UpdateStudentEmail(string id, string email)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

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

        // evoCampus related
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

       
        // Internship related
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

        // Assignment related
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

        // Eltávolítás a listából
        public void DeleteStudent(string id)
        {
            // Megkeressük az elemet
            var studentToRemove = _students.FirstOrDefault(s => s.StudID == id);

            // Ha létezik, kitöröljük
            if (studentToRemove != null)
            {
                _students.Remove(studentToRemove);
            }
        }
    }
}
