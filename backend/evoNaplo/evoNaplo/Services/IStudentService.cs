using evoNaplo.DTO;
using evoNaplo.Models;

namespace evoNaplo.Services;

public interface IStudentService
{
    Student? GetStudentModelById(string id);
    Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
    Task<StudentDTO> GetStudentByIdAsync(string id);
    Task<StudentDTO> AddStudentAsync(StudentDTO studentToAdd);
    Task<StudentDTO> UpdateStudentAsync(string id, StudentDTO updatedStudent);
    Task<bool> DeleteStudentAsync(string id);
    
}
