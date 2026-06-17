using evoNaplo.DAL.Interfaces;
using evoNaplo.Data;
using evoNaplo.Exceptions;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;

namespace evoNaplo.DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student?> GetStudentByIdAsync(string id)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
        } 
        public async Task<Student> AddStudentAsync(Student student)
        {
            if (string.IsNullOrEmpty(student.Id))
            {
                student.Id = Nanoid.Generate();
            }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }
        public async Task<Student> UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            
            return student;
        }
        public async Task<bool> DeleteStudentAsync(string id)
        {
            var existingStudent = await GetStudentByIdAsync(id);

            if (existingStudent is null)
            {
                throw new StudentNotFoundException(id);
            }

            _context.Students.Remove(existingStudent);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
