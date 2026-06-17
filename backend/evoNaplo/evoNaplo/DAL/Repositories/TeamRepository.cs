using evoNaplo.DAL.Interfaces;
using evoNaplo.Data;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using evoNaplo.Exceptions;

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
            return await _context.Teams.ToListAsync();
        }
        public async Task<Team?> GetTeamByIdAsync(string id) {
            return await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
        } 
        public async Task<Team> AddTeamAsync(Team team)
        {
            if (string.IsNullOrEmpty(team.Id))
            {
                team.Id = Nanoid.Generate();
            }
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return team;
        }
        public async Task<Team> UpdateTeamAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();

            return team;
        }
        public async Task<bool> DeleteTeamAsync(string id)
        {
            var existingTeam = await GetTeamByIdAsync(id);

            if (existingTeam is null)
            {
                throw new TeamNotFoundException(id);
            }
            _context.Remove(existingTeam);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
