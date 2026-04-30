using evoNaplo.Services;
using System.Collections.Generic;
using System.Linq;
using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal interface IStudentService
    {
            IEnumerable<StudentDTO> GetAllStudents();
            StudentDTO? GetStudentById(string id);
            void AddStudent(StudentDTO student);
            void UpdateStudent(string id, StudentDTO updatedStudent);
            void DeleteStudent(string id);
            
    }
}
