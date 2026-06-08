using evoNaplo.DAL.Interfaces;
using evoNaplo.Data;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;

namespace evoNaplo.DAL.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams.Include(t => t.Mentors)
                .Include(t => t.Students)
                .Include(t => t.Project)
                .Include(t => t.AttendanceSheets)
                .AsSplitQuery()
                .ToListAsync();
        }
        public async Task<Team?> GetTeamByIdAsync(string id) {
            return await _context.Teams
                .Include(t => t.Students)
                .Include(t => t.Project)
                .Include(t => t.AttendanceSheets)
                .AsSplitQuery()
                .FirstOrDefaultAsync(t => t.Id == id);
        } 
        public async Task AddTeamAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTeamAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTeamAsync(string id)
        {
            var existingTeam = await GetTeamByIdAsync(id);
            if (existingTeam is not null)
            {
                _context.Teams.Remove(existingTeam);
                await _context.SaveChangesAsync();
            }
        }
    }
}
