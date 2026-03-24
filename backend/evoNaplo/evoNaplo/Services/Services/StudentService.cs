using evoNaplo.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace evoNaplo.Services.Services
{
    public class StudentService : Interface.IStudentService
    {
        // STATIKUS adattagok: Ezek a memóriában maradnak a program leállásáig.
        private static readonly List<Student> _students = new List<Student>();
        private static int _nextId = 1;

        // Visszaadja a listában lévő összes entitást
        public List<Student> GetAllStudents()
        {
            return _students;
        }

        // Keresés ID alapján. Ha nem találja, null értékkel tér vissza.
        public Student GetStudentById(int id)
        {
            return _students.FirstOrDefault(s => s.StudID == id);
        }

        // Hozzáadás a listához
        public void AddStudent(Student student)
        {
            // Beállítjuk a következő szabad ID-t, majd megnöveljük a számlálót
            student.StudID = _nextId++;
            _students.Add(student);
        }

        // Módosítás




        // Részleges frissítés: csak a megadott (nem null) mezőket frissítjük
        public void UpdateStudentFields(int id, Student updatedFields)
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
        public void UpdateStudentIsFirstSemester(int id, bool isFirstSemester)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.IsFirstSemester = isFirstSemester;
        }

        public void UpdateStudentStayWithCurrentTeam(int id, bool stayWithCurrentTeam)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StayWithCurrentTeam = stayWithCurrentTeam;
        }

        // Single-field update methods
        public void UpdateStudentName(int id, string name)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StudName = name;
        }

        public void UpdateStudentEmail(int id, string email)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StudEmail = email;
        }

        public void UpdateStudentPhoneNumber(int id, string phoneNumber)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StudPhoneNumber = phoneNumber;
        }

        public void UpdateStudentCurrentStudies(int id, string currentStudies)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.CurrentStudies = currentStudies;
        }

        public void UpdateStudentPersonalGoals(int id, string personalGoals)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.PersonalGoals = personalGoals;
        }

        // evoCampus related
        public void UpdateStudentAppliedForCampus(int id, bool appliedForCampus)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.AppliedForCampus = appliedForCampus;
        }

        public void UpdateStudentActiveScholarship(int id, bool activeScholarship)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.ActiveScholarship = activeScholarship;
        }

        public void UpdateStudentScholarshipDuration(int id, int scholarshipDuration)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.ScholarshipDuration = scholarshipDuration;
        }

       
        // Internship related
        public void UpdateStudentInternshipApplied(int id, bool internshipApplied)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.InternshipApplied = internshipApplied;
        }

        public void UpdateStudentIsAnIntern(int id, bool isAnIntern)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.IsAnIntern = isAnIntern;
        }

        public void UpdateStudentDoesWork(int id, bool doesWork)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.doeswork = doesWork;
        }

        public void UpdateStudentWorkDuration(int id, string workDuration)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.WorkDuration = workDuration;
        }

        // Assignment related
        public void UpdateStudentAssignedTeam(int id, string assignedTeam)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StudAssignedTeam = assignedTeam;
        }

        public void UpdateStudentAssignedProject(int id, string assignedProject)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudID == id);
            if (existingStudent == null) return;

            existingStudent.StudAssignedProject = assignedProject;
        }

        // Eltávolítás a listából
        public void DeleteStudent(int id)
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
