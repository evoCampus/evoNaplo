using evoNaplo.Services;

namespace evoNaplo.Services.Interface
{
    internal interface IStudentService
    {
       

            

            IEnumerable<Student> GetAllStudents();
            Student GetStudentById(string id);
            void AddStudent(Student student);
            void UpdateStudent(string id, Student updatedStudent);
            void DeleteStudent(string id);

    }
}
