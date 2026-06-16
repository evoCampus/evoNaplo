using evoNaplo.DTO.ProjectDTOs;
using evoNaplo.Models;

namespace evoNaplo.Services;

public interface IProjectService
{
    Task<Project> GetProjectModelById(string id);
    Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync();
    Task<ProjectDTO> GetProjectByIdAsync(string id);
    Task<ProjectDTO> AddProjectAsync(CreateProjectDTO projectToAdd);
    Task<ProjectDTO> UpdateProjectAsync(string id, UpdateProjectDTO updatedProject);
    Task<bool> DeleteProjectAsync(string id);
    
}
