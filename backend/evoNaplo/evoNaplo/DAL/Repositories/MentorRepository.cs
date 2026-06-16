using evoNaplo.DAL.Interfaces;
using evoNaplo.Data;
using evoNaplo.Exceptions;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;

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
            return await _context.Mentors.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Mentor> AddMentorAsync(Mentor mentor)
        {
            if (string.IsNullOrEmpty(mentor.Id))
            {
                mentor.Id = Nanoid.Generate();
            }
            await _context.Mentors.AddAsync(mentor);
            await _context.SaveChangesAsync();

            return mentor;
        }

        public async Task<Mentor> UpdateMentorAsync(Mentor mentor)
        {
            _context.Mentors.Update(mentor);
            await _context.SaveChangesAsync();

            return mentor;
        }

        public async Task<bool> DeleteMentorAsync(string id)
        {
            var existingMentor = await GetMentorByIdAsync(id);

            if (existingMentor is null)
            {
                throw new MentorNotFoundException(id);
            }

            _context.Mentors.Remove(existingMentor);
            await _context.SaveChangesAsync();  

            return true;
        }
        public async Task<Mentor?> GetMentorsWithDetails(string id)
        {
            return await _context.Mentors
                .Include(t => t.Teams)
                .Include(t => t.Projects)
                .AsSplitQuery()
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
