using evoNaplo.DTO.StudentDTOs;
using evoNaplo.Models;

namespace evoNaplo.Services;

public interface IStudentService
{
    Task<Student> GetStudentModelById(string id);
    Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
    Task<StudentDTO> GetStudentByIdAsync(string id);
    Task<StudentDTO> AddStudentAsync(CreateStudentDTO studentToAdd);
    Task<StudentDTO> UpdateStudentAsync(string id, UpdateStudentDTO updatedStudent);
    Task<bool> DeleteStudentAsync(string id);
    
}
