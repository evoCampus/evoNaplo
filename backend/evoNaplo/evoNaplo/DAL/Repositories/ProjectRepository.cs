using evoNaplo.Controllers;
using evoNaplo.DAL.Interfaces;
using evoNaplo.Data;
using evoNaplo.Exceptions;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;

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
            return await _context.Projects
                .Include(p => p.Teams)
                .Include(p => p.ProjectLinks)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(string id)
        {
            return await _context.Projects
                .Include(p => p.Teams)
                .Include(p => p.ProjectLinks)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            if (string.IsNullOrEmpty(project.Id))
            {
                project.Id = Nanoid.Generate();
            }
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project> AddTeamToProjectAsync(string projectId, Team team)
        {
            var project = await _context.Projects
                .Include(p => p.Teams)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project is null)
            {
                throw new ProjectNotFoundException(projectId);
            }

            project.Teams ??= new List<Team>();

            if (!project.Teams.Any(t => t.Id == team.Id))
            {
                project.Teams.Add(team);
            }

            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<bool> RemoveTeamFromProjectAsync(string projectId, Team team)
        {
            var project = await _context.Projects.Include(p => p.Teams).FirstOrDefaultAsync(p => p.Id == projectId);

            if (project is null)
            {
                throw new ProjectNotFoundException(projectId);
            }

            project.Teams ??= new List<Team>();

            var teamToRemove = project.Teams.FirstOrDefault(t => t.Id == team.Id);

            if (teamToRemove is not null)
            {
                project.Teams.Remove(teamToRemove);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProjectAsync(string id)
        {
            var existingProject = await GetProjectByIdAsync(id);

            if (existingProject is null)
            {
                throw new ProjectNotFoundException(id);
            }

            _context.Remove(existingProject);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Project?> GetProjectWithDetailsAsync(string id)
        {
            return await _context.Projects
                .Include(t => t.Teams)
                .Include(t => t.ProjectLinks)
                .AsSplitQuery()
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
