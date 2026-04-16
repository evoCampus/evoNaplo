using evoNaplo.Services;
using System.Collections.Generic;
using System.Linq;
using evoNaplo.Models;

namespace evoNaplo.Services
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
