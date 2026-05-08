using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal interface IStudentService
    {
        public Student? GetStudentModelById(string id);
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(string id);
        Task<StudentDTO> AddStudentAsync(StudentDTO student);
        Task<StudentDTO> UpdateStudentAsync(string id, StudentDTO updatedStudent);
        Task<bool> DeleteStudentAsync(string id);

    }
}
