using evoNaplo.DAL.Interfaces;
using evoNaplo.DTO;
using evoNaplo.Exceptions;
using evoNaplo.Models;

namespace evoNaplo.Services;

/// <summary>
/// The ProjectService class provides methods for managing projects in the application. It allows for retrieving, adding, updating, and deleting projects. The service uses an in-memory list to store project data and interacts with the team service to manage relationships between projects and teams. The service also includes error handling to ensure that appropriate exceptions are thrown when operations fail, such as when a project is not found or when trying to add a project that already exists.
/// </summary>
internal class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITeamRepository _teamRepository;

    public ProjectService(ITeamRepository teamRepository, IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
        _teamRepository = teamRepository;
    }

    /// <summary>
    /// Retrieves a project model by its ID. If a project with the specified ID is found in the list of projects, it is returned. If no project is found with the given ID, a ProjectNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the project to retrieve.</param>
    /// <returns>The Project model if found.</returns>
    /// <exception cref="ProjectNotFoundException"></exception>
    public async Task<Project> GetProjectModelById(string id)
    {
        var project = await _projectRepository.GetProjectByIdAsync(id);
        if (project is null)
        {
            throw new ProjectNotFoundException($"Project with ID {id} not found.");
        }
        return project;
    }

    /// <summary>
    /// Retrieves all projects as a collection of ProjectDTOs. The method iterates through the list of projects and converts each project model into a ProjectDTO, which is then returned as an IEnumerable collection. This allows for a more structured and simplified representation of project data when it is accessed by other parts of the application.
    /// </summary>
    /// <returns>An IEnumerable collection of ProjectDTOs representing all projects.</returns>
    public async Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllProjectsAsync();
        return projects.Select(p => new ProjectDTO(p));
    }

    /// <summary>
    /// Retrieves a project by its ID and returns it as a ProjectDTO. If a project with the specified ID is found in the list of projects, it is converted into a ProjectDTO and returned. If no project is found with the given ID, a ProjectNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the project to retrieve.</param>
    /// <returns>The ProjectDTO if found.</returns>
    /// <exception cref="ProjectNotFoundException"></exception>
    public async Task<ProjectDTO> GetProjectByIdAsync(string id)
    {
        var project = await _projectRepository.GetProjectByIdAsync(id);
        if (project is not null) 
        {
            return new ProjectDTO(project);
        }
        throw new ProjectNotFoundException($"Project with ID {id} not found.");
    }

    /// <summary>
    /// Adds a new project to the list of projects. The method takes a ProjectDTO as input, generates a new unique ID for the project, and creates a new Project model based on the provided DTO. The new project is then added to the list of projects, and the original ProjectDTO (with the newly assigned ID) is returned. This allows for the creation of new project entries in the application while ensuring that each project has a unique identifier.
    /// </summary>
    /// <param name="projectToAddDTO">The ProjectDTO to add.</param>
    /// <returns>The added ProjectDTO if successful.</returns>
    public async Task<ProjectDTO> AddProjectAsync(ProjectDTO projectToAddDTO)
    {
        var newProject = new Project
        {
            Id = projectToAddDTO.Id,
            Name = projectToAddDTO.Name,
            ShortDescription = projectToAddDTO.Description,
            ProjectLinks = projectToAddDTO.ProjectLinks is not null ? projectToAddDTO.ProjectLinks
            .Select(l => new ProjectLink
            {
                Id = Guid.NewGuid().ToString(),
                LinkType = Enum.TryParse<LinkTypes>(l.Key, out var type) ? type : LinkTypes.GitHub,
                Url = l.Value,
                ProjectId = projectToAddDTO.Id
            })
            .ToList()
            : new List<ProjectLink>(),

            Teams = new List<Team>()
        };


        if (projectToAddDTO.TeamIds is not null)
        {
            foreach (var teamId in projectToAddDTO.TeamIds)
            {
                var team = await _teamRepository.GetTeamByIdAsync(teamId);
                if (team is not null)
                {
                    team.ProjectId = newProject.Id;

                    await _teamRepository.UpdateTeamAsync(team);

                    newProject.Teams.Add(team);
                }
            }
        }

        await _projectRepository.AddProjectAsync(newProject);

        projectToAddDTO.Id = newProject.Id;
        return projectToAddDTO;
    }

    /// <summary>
    /// Updates an existing project with the specified ID using the provided ProjectDTO. The method first checks if a project with the given ID exists in the list of projects. If a project is found, its properties are updated with the values from the provided ProjectDTO, and the updated ProjectDTO is returned. If no project is found with the specified ID, a ProjectNotFoundException is thrown with an appropriate message. This allows for modifying existing project entries in the application while ensuring that only valid projects can be updated.
    /// </summary>
    /// <param name="id">The ID of the project to update.</param>
    /// <param name="updatedProjectDTO">The updated project DTO.</param>
    /// <returns>The updated ProjectDTO if successful.</returns>
    /// <exception cref="ProjectNotFoundException"></exception>
    public async Task<ProjectDTO> UpdateProjectAsync(string id, ProjectDTO updatedProjectDTO)
    {
        var existingProject = await _projectRepository.GetProjectByIdAsync(id);
        if (existingProject is not null) 
        {
            existingProject.Teams ??= new List<Team>();
            existingProject.ProjectLinks ??= new List<ProjectLink>();   
            existingProject.Name = updatedProjectDTO.Name;
            existingProject.ShortDescription = updatedProjectDTO.Description;

            existingProject.ProjectLinks.Clear();

            if (updatedProjectDTO.ProjectLinks is not null) {
                foreach (var l in updatedProjectDTO.ProjectLinks)
                {
                    existingProject.ProjectLinks.Add(new ProjectLink
                    {
                        Id = Guid.NewGuid().ToString(),
                        LinkType = Enum.TryParse<LinkTypes>(l.Key, out var type) ? type : LinkTypes.GitHub,
                        Url = l.Value,
                        ProjectId = existingProject.Id
                    });
                }
            }
            if (updatedProjectDTO.TeamIds is not null)
            {
                var teamsToRemove = existingProject.Teams
                .Where(t => !updatedProjectDTO.TeamIds.Contains(t.Id))
                .ToList();

                foreach (var teamToRemove in teamsToRemove)
                {
                    existingProject.Teams.Remove(teamToRemove);
                }
                foreach (var teamId in updatedProjectDTO.TeamIds)
                {
                    if (!existingProject.Teams.Any(t => t.Id == teamId))
                    {
                        var team = await _teamRepository.GetTeamByIdAsync(teamId);
                        if (team is not null)
                        {
                            team.ProjectId = existingProject.Id;
                            await _teamRepository.UpdateTeamAsync(team);

                            existingProject.Teams.Add(team);
                        }
                    }
                }
            }

            await _projectRepository.UpdateProjectAsync(existingProject);
            updatedProjectDTO.Id = existingProject.Id;
            return updatedProjectDTO;
        }
        throw new ProjectNotFoundException($"Project with ID {id} not found.");
    }
    
    /// <summary>
    /// Deletes a project with the specified ID from the list of projects. The method checks if a project with the given ID exists in the list. If a project is found, it is removed from the list, and the method returns true to indicate that the deletion was successful. If no project is found with the specified ID, a ProjectNotFoundException is thrown with an appropriate message. This allows for the removal of project entries from the application while ensuring that only valid projects can be deleted.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    /// <returns>A boolean indicating whether the project was deleted.</returns>
    /// <exception cref="ProjectNotFoundException"></exception>
    public async Task<bool> DeleteProjectAsync(string id)
    {
        await _projectRepository.DeleteProjectAsync(id);
        return true;
    }
    
}
