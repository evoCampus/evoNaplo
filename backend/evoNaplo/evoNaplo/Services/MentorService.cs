using evoNaplo.Models;
using evoNaplo.Exceptions;
using evoNaplo.DAL.Interfaces;
using evoNaplo.DTO.MentorDTOs;

namespace evoNaplo.Services;

/// <summary>
/// The MentorService class provides methods for managing mentors in the application. It allows for retrieving, adding, updating, and deleting mentors. The service uses an in-memory list to store mentor data and interacts with team and project services to manage relationships between mentors, teams, and projects. The service also includes error handling to ensure that appropriate exceptions are thrown when operations fail, such as when a mentor is not found or when trying to add a mentor that already exists.
/// </summary>
internal class MentorService : IMentorService
{
    private readonly IMentorRepository _mentorRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IProjectRepository _projectRepository;
    
    public MentorService(ITeamRepository teamRepository, IProjectRepository projectRepository, IMentorRepository mentorRepository)
    {
        _mentorRepository = mentorRepository;
        _teamRepository = teamRepository;
        _projectRepository = projectRepository;
    }
    
    /// <summary>
    /// Retrieves a mentor model by its ID. If a mentor with the specified ID is found in the list of mentors, it is returned. If no mentor is found with the given ID, a MentorNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the mentor to retrieve.</param>
    /// <returns>The Mentor model if found.</returns>
    /// <exception cref="MentorNotFoundException"></exception>
    public async Task<Mentor?> GetMentorModelById(string id)
    {
        var mentor = await _mentorRepository.GetMentorByIdAsync(id);
        if (mentor is null)
        {
            throw new MentorNotFoundException($"Mentor with ID {id} not found.");
        }
        return mentor;
    }
    
    /// <summary>
    /// Retrieves all mentors as a collection of MentorDTOs. The method iterates through the list of mentors and converts each mentor model into a MentorDTO, which is then returned as an IEnumerable collection. This allows for a more structured and simplified representation of mentor data when it is accessed by other parts of the application.
    /// </summary>
    /// <returns>An IEnumerable collection of MentorDTOs representing all mentors.</returns>
    public async Task<IEnumerable<MentorDTO>> GetAllMentorsAsync()
    {
        var mentor = await _mentorRepository.GetAllMentorsAsync();
        return mentor?.Select(m => new MentorDTO(m)) ?? Enumerable.Empty<MentorDTO>();
    }

    /// <summary>
    /// Retrieves a mentor by its ID and returns it as a MentorDTO. If a mentor with the specified ID is found in the list of mentors, it is converted into a MentorDTO and returned. If no mentor is found with the given ID, a MentorNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the mentor to retrieve.</param>
    /// <returns>The MentorDTO if found.</returns>
    /// <exception cref="MentorNotFoundException"></exception>
    public async Task<MentorDetailsDTO> GetMentorByIdAsync(string id)
    {
        var mentor = await _mentorRepository.GetMentorByIdAsync(id);
        if (mentor is null)
        {
            throw new MentorNotFoundException($"Mentor with ID {id} not found.");
        }
        return new MentorDetailsDTO(mentor);
    }

    /// <summary>
    /// Adds a new mentor to the list of mentors. The method takes a MentorDTO as input, generates a new unique ID for the mentor, and creates a new Mentor model based on the provided DTO. The new mentor is then added to the list of mentors, and the original MentorDTO (with the newly assigned ID) is returned. This allows for the creation of new mentor entries in the application while ensuring that each mentor has a unique identifier.
    /// </summary>
    /// <param name="mentorToAdd">The MentorDTO to add.</param>
    /// <returns>The added MentorDTO if successful.</returns>

    public async Task<MentorDTO> AddMentorAsync(CreateMentorDTO mentorToAddDTO)
    {
        var newMentor = mentorToAddDTO.toMentor();

        if (!string.IsNullOrEmpty(mentorToAddDTO.TeamId))
        {
            var team = await _teamRepository.GetTeamByIdAsync(mentorToAddDTO.TeamId);
            if (team is not null)
            {
                newMentor.Teams.Add(team);
            }
        }

        if (!string.IsNullOrEmpty(mentorToAddDTO.ProjectId))
        {
            var project = await _projectRepository.GetProjectByIdAsync(mentorToAddDTO.ProjectId);
            if (project is not null)
            {
                newMentor.Projects.Add(project);
            }
        }

        var addedMentor = await _mentorRepository.AddMentorAsync(newMentor);
            
        return new MentorDTO(addedMentor);
    }

    /// <summary>
    /// Updates an existing mentor with the specified ID using the provided MentorDTO. The method first checks if a mentor with the given ID exists in the list of mentors. If a mentor is found, its properties are updated with the values from the provided MentorDTO, and the updated MentorDTO is returned. If no mentor is found with the specified ID, a MentorNotFoundException is thrown with an appropriate message. This allows for modifying existing mentor entries in the application while ensuring that only valid mentors can be updated.
    /// </summary>
    /// <param name="id">The ID of the mentor to update.</param>
    /// <param name="updatedMentor">The updated mentor DTO.</param>
    /// <returns>The updated MentorDTO if successful.</returns>
    /// <exception cref="MentorNotFoundException"></exception>
    public async Task<MentorDTO> UpdateMentorAsync(string id, UpdateMentorDTO updatedMentorDTO)
    {
        var existingMentor = await _mentorRepository.GetMentorsWithDetails(id);
        if (existingMentor is not null)
        {
            existingMentor.Name = updatedMentorDTO.Name;
            existingMentor.Email = updatedMentorDTO.Email;
            existingMentor.PhoneNumber = updatedMentorDTO.PhoneNumber;
            existingMentor.Teams ??= new List<Team>();
            existingMentor.Projects ??= new List<Project>();

            if (!string.IsNullOrEmpty(updatedMentorDTO.TeamId))
            {
                existingMentor.Teams.Clear();
                var team = await _teamRepository.GetTeamByIdAsync(updatedMentorDTO.TeamId);
                if (team is not null)
                {
                    existingMentor.Teams.Add(team);
                }
            }

            if (!string.IsNullOrEmpty(updatedMentorDTO.ProjectId))
            {
                existingMentor.Projects.Clear();
                var project = await _projectRepository.GetProjectByIdAsync(updatedMentorDTO.ProjectId);
                if (project is not null)
                {
                    existingMentor.Projects.Add(project);
                }
            }
            await _mentorRepository.UpdateMentorAsync(existingMentor);

            return new MentorDTO(existingMentor);
        }
        
            throw new MentorNotFoundException($"Mentor with ID {id} not found.");
    }

    /// <summary>
    /// Deletes a mentor with the specified ID from the list of mentors. The method checks if a mentor with the given ID exists in the list. If a mentor is found, it is removed from the list, and the method returns true to indicate that the deletion was successful. If no mentor is found with the specified ID, a MentorNotFoundException is thrown with an appropriate message. This allows for the removal of mentor entries from the application while ensuring that only valid mentors can be deleted.
    /// </summary>
    /// <param name="id">The ID of the mentor to delete.</param>
    /// <returns>A boolean indicating whether the mentor was deleted.</returns>
    /// <exception cref="MentorNotFoundException"></exception>
    public async Task<bool> DeleteMentorAsync(string id)
    {
        await _mentorRepository.DeleteMentorAsync(id);
        return true;
    }
}
