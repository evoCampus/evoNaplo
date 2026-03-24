using evoNaplo.Services.Models;

namespace evoNaplo.Services.Interface
{
    public interface IStudentService
    {
       

            

            List<Student> GetAllStudents();

            
            Student GetStudentById(int id);

            
            void AddStudent(Student student);

            
            

            // Update only the non-null fields of a student identified by id
            void UpdateStudentFields(int id, Student updatedFields);

            // Update boolean properties separately (non-nullable in model)
            void UpdateStudentIsFirstSemester(int id, bool isFirstSemester);

            void UpdateStudentStayWithCurrentTeam(int id, bool stayWithCurrentTeam);

            // Update single fields separately
            void UpdateStudentName(int id, string name);
            void UpdateStudentEmail(int id, string email);
            void UpdateStudentPhoneNumber(int id, string phoneNumber);
            void UpdateStudentCurrentStudies(int id, string currentStudies);
            void UpdateStudentPersonalGoals(int id, string personalGoals);

            // evoCampus related
            void UpdateStudentAppliedForCampus(int id, bool appliedForCampus);
            void UpdateStudentActiveScholarship(int id, bool activeScholarship);
            void UpdateStudentScholarshipDuration(int id, int scholarshipDuration);

            // Assignment related
            void UpdateStudentAssignedTeam(int id, string assignedTeam);
            void UpdateStudentAssignedProject(int id, string assignedProject);
            

            // Internship related
            void UpdateStudentInternshipApplied(int id, bool internshipApplied);
            void UpdateStudentIsAnIntern(int id, bool isAnIntern);
            void UpdateStudentDoesWork(int id, bool doesWork);
            void UpdateStudentWorkDuration(int id, string workDuration);

            
            void DeleteStudent(int id);

    }
}
