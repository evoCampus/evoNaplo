using evoNaplo.DAL.Interfaces;
using evoNaplo.Data;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Students
                .Include(s => s.Team)
                .ToListAsync();
        }
        public async Task<Student?> GetStudentByIdAsync(string id)
        {
            return await _context.Students
                .Include(t => t.Team)
                .FirstOrDefaultAsync(x => x.Id == id);
        } 
        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteStudentAsync(string id)
        {
            var existingStudent = await GetStudentByIdAsync(id);
            if (existingStudent is not null)
            {
                _context.Students.Remove(existingStudent);
                await _context.SaveChangesAsync();
            }
        }
    }
}
