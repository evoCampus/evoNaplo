using evoNaplo.Services;

namespace evoNaplo.Services.Interface
{
    public interface IStudentService
    {
       

            

            List<Student> GetAllStudents();

            
            Student GetStudentById(string id);

            
            void AddStudent(Student student);

            
            

            // Update only the non-null fields of a student identified by id
            void UpdateStudentFields(string id, Student updatedFields);

            // Update boolean properties separately (non-nullable in model)
            void UpdateStudentIsFirstSemester(string id, bool isFirstSemester);

            void UpdateStudentStayWithCurrentTeam(string id, bool stayWithCurrentTeam);

            // Update single fields separately
            void UpdateStudentName(string id, string name);
            void UpdateStudentEmail(string id, string email);
            void UpdateStudentPhoneNumber(string id, string phoneNumber);
            void UpdateStudentCurrentStudies(string id, string currentStudies);
            void UpdateStudentPersonalGoals(string id, string personalGoals);

            // evoCampus related
            void UpdateStudentAppliedForCampus(string id, bool appliedForCampus);
            void UpdateStudentActiveScholarship(string id, bool activeScholarship);
            void UpdateStudentScholarshipDuration(string id, int scholarshipDuration);

            // Assignment related
            void UpdateStudentAssignedTeam(string id, string assignedTeam);
            void UpdateStudentAssignedProject(string id, string assignedProject);
            

            // Internship related
            void UpdateStudentInternshipApplied(string id, bool internshipApplied);
            void UpdateStudentIsAnIntern(string id, bool isAnIntern);
            void UpdateStudentDoesWork(string id, bool doesWork);
            void UpdateStudentWorkDuration(string id, string workDuration);

            
            void DeleteStudent(string id);

    }
}
