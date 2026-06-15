using evoNaplo.Models;

namespace evoNaplo.DAL.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(string id);
        Task<Project> AddProjectAsync(Project project);
        Task<Project> UpdateProjectAsync(Project project);
        Task<Project> AddTeamsToProjectAsync(string projectId, IEnumerable<Team> teams);
        Task<bool> RemoveTeamsFromProjectAsync(string projectId, IEnumerable<Team> teams);
        Task<bool> DeleteProjectAsync(string id);
    }
}
