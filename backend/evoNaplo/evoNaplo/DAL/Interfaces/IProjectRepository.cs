using evoNaplo.Models;

namespace evoNaplo.DAL.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(string id);
        Task<Project> AddProjectAsync(Project project);
        Task<Project> UpdateProjectAsync(Project project);
        Task<Project> AddTeamToProjectAsync(string projectId, Team team);
        Task<bool> RemoveTeamFromProjectAsync(string projectId, Team team);
        Task<bool> DeleteProjectAsync(string id);
        Task<Project?> GetProjectWithDetailsAsync(string id);
    }
}
