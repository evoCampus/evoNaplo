using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal interface IProjectService
    {
        Project? GetProjectModelById(string id);
        Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync();
        Task<ProjectDTO> GetProjectByIdAsync(string id);
        Task<ProjectDTO> AddProjectAsync(ProjectDTO projectToAdd);
        Task<ProjectDTO> UpdateProjectAsync(string id, ProjectDTO updatedProject);
        Task<bool> DeleteProjectAsync(string id);

    }
}
