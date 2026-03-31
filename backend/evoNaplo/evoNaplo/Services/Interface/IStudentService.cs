using evoNaplo.Services;

namespace evoNaplo.Services.Interface
{
    public interface IStudentService
    {
            List<Student> GetAllStudents();            
            Student GetStudentById(string id);            
            void AddStudent(Student student);
            void UpdateStudentFields(string id, Student updatedFields);
            void UpdateStudent(string id, Student updatedStudent);
            void DeleteStudent(string id);

    }
}
