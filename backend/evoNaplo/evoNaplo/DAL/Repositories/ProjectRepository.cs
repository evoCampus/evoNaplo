using evoNaplo.DAL.Interfaces;
using evoNaplo.Data;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;

namespace evoNaplo.DAL.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(string id)
        {
            return await _context.Projects
                .Include(t => t.Teams)
                .Include(pr => pr.ProjectLinks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task AddTeamsToProjectAsync(string projectId, IEnumerable<Team> teams)
        {
            var project = await _context.Projects.Include(p => p.Teams).FirstOrDefaultAsync(p => p.Id == projectId);

            if (project is not null && project.Teams is not null)
            {
                foreach (var team in teams)
                {
                    if (!project.Teams.Any(t => t.Id == team.Id))
                    {
                        project.Teams.Add(team);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveTeamsFromProjectAsync(string projectId, IEnumerable<Team> teams)
        {
            var project = await _context.Projects.Include(p => p.Teams).FirstOrDefaultAsync(p => p.Id == projectId);

            if (project is not null && project.Teams is not null)
            {
                foreach (var team in teams)
                {
                    var teamsToRemove = project.Teams.FirstOrDefault(t => t.Id == team.Id);
                    if (teamsToRemove is not null)
                    {
                        project.Teams.Remove(teamsToRemove);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProjectAsync(string id)
        {
            var existingProject = await GetProjectByIdAsync(id);
            if (existingProject is not null)
            {
                _context.Projects.Remove(existingProject);
                await _context.SaveChangesAsync();
            }
        }
    }
}
