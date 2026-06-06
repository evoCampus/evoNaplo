using evoNaplo.DTO;
using evoNaplo.Models;

namespace evoNaplo.Services;

public interface IProjectService
{
    Task<Project> GetProjectModelById(string id);
    Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync();
    Task<ProjectDTO> GetProjectByIdAsync(string id);
    Task<ProjectDTO> AddProjectAsync(ProjectDTO projectToAdd);
    Task<ProjectDTO> UpdateProjectAsync(string id, ProjectDTO updatedProject);
    Task<bool> DeleteProjectAsync(string id);
    
}
