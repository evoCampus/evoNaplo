using evoNaplo.Models;

namespace evoNaplo.DAL.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(string id);
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task AddTeamsToProjectAsync(string projectId, IEnumerable<Team> teams);
        Task RemoveTeamsFromProjectAsync(string projectId, IEnumerable<Team> teams);
        Task DeleteProjectAsync(string id);
    }
}
