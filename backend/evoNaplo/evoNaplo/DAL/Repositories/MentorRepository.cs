using evoNaplo.DAL.Interfaces;
using evoNaplo.Data;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;

namespace evoNaplo.DAL.Repositories
{
    public class MentorRepository : IMentorRepository
    {
        private readonly AppDbContext _context;

        public MentorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Mentor>> GetAllMentorsAsync()
        {
            return await _context.Mentors.ToListAsync();
        }
        public async Task<Mentor?> GetMentorByIdAsync(string id)
        {
            return await _context.Mentors
               .Include(m => m.Teams)
               .Include(m => m.Projects)
               .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddMentorAsync(Mentor mentor)
        {
            await _context.Mentors.AddAsync(mentor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMentorAsync(Mentor mentor)
        {
            _context.Mentors.Update(mentor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMentorAsync(string id)
        {
            var existingMentor = await GetMentorByIdAsync(id);
            if (existingMentor is not null)
            {
                _context.Mentors.Remove(existingMentor);
                await _context.SaveChangesAsync();
            }
        }


    }
}
