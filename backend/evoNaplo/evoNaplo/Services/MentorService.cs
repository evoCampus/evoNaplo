using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal class MentorService : IMentorService
    {
        private static readonly List<Mentor> _mentors = new List<Mentor>();
        private readonly ITeamService _teamService;
        private readonly IProjectService _projectService;
        
        public MentorService(ITeamService teamService, IProjectService projectService)
        {
            _teamService = teamService;
            _projectService = projectService;
        }

        /// <summary>
        /// This method is used internally by the service to retrieve the Mentor model for operations that require it, such as adding or updating mentors. It is not intended to be exposed directly to clients, as it returns the internal model rather than a DTO.
        /// </summary>
        /// <param name="id">The ID of the mentor to retrieve.</param>
        /// <returns>The Mentor model if found, otherwise null.</returns>
        public Mentor? GetMentorModelById(string id)
        {
            return _mentors.FirstOrDefault(m => m.Id == id);
        }
        
        /// <summary>
        /// Retrieves a list of all mentors in the database. If no mentors are found, a MentorNotFoundException is thrown with an appropriate message. Each mentor is returned as a MentorDTO.
        /// </summary>
        /// <returns>A list of MentorDTOs if mentors are found.</returns>
        /// <exception cref="MentorNotFoundException"></exception>
        public async Task<IEnumerable<MentorDTO>> GetAllMentorsAsync()
        {
            IEnumerable<MentorDTO> mentors = _mentors.Select(m => new MentorDTO(m));
            if (mentors.Any())
            {
                return mentors;
            }
            throw new MentorNotFoundException("No mentors found.");
        }

        /// <summary>
        /// Retrieves a specific mentor by their ID. If a mentor with the specified ID is found, it is returned as a MentorDTO. If no mentor is found with the given ID, a MentorNotFoundException is thrown with an appropriate message.
        /// </summary>
        /// <param name="id">The ID of the mentor to retrieve.</param>
        /// <returns>The MentorDTO if found.</returns>
        /// <exception cref="MentorNotFoundException"></exception>
        public async Task<MentorDTO> GetMentorByIdAsync(string id)
        {
            Mentor? mentor = _mentors.FirstOrDefault(m => m.Id == id);
            if (mentor is not null) 
            {
                return new MentorDTO(mentor);
            }
            throw new MentorNotFoundException($"Mentor with ID {id} not found.");
        }

        /// <summary>
        /// Adds a new mentor to the list. If a mentor with the same ID already exists, a MentorAlreadyExistsException is thrown with an appropriate message. If the mentor is added successfully, the provided MentorDTO is returned. If the ID of the mentor to add is null or empty, a new GUID will be generated and assigned as the ID.
        /// </summary>
        /// <param name="mentorToAdd">The MentorDTO to add.</param>
        /// <returns>The added MentorDTO if successful.</returns>
        /// <exception cref="MentorAlreadyExistsException"></exception>
        public async Task<MentorDTO> AddMentorAsync(MentorDTO mentorToAdd)
        {
            if (string.IsNullOrEmpty(mentorToAdd.Id)) 
            {
                mentorToAdd.Id = Guid.NewGuid().ToString();
            }
            if (_mentors.Any(m => m.Id == mentorToAdd.Id))
            {
                throw new MentorAlreadyExistsException($"A mentor with the ID {mentorToAdd.Id} already exists.");
            }
            _mentors.Add(new Mentor
            {
                Id = mentorToAdd.Id,
                Name = mentorToAdd.Name,
                Email = mentorToAdd.Email,
                PhoneNumber = mentorToAdd.PhoneNumber,
                Teams = mentorToAdd.TeamIds.Select(_teamService.GetTeamModelById).OfType<Team>().ToList(),
                Projects = mentorToAdd.ProjectIds.Select(_projectService.GetProjectModelById).OfType<Project>().ToList(),
            });
            return mentorToAdd;
        }

        /// <summary>
        /// Updates an existing mentor with the specified ID using the provided updated mentor data. If a mentor with the given ID is found, it is updated with the new data and the updated MentorDTO is returned. If no mentor is found with the specified ID, a MentorNotFoundException is thrown with an appropriate message.
        /// </summary>
        /// <param name="id">The ID of the mentor to update.</param>
        /// <param name="updatedMentor">The updated mentor DTO.</param>
        /// <returns>The updated MentorDTO if successful.</returns>
        /// <exception cref="MentorNotFoundException"></exception>
        public async Task<MentorDTO> UpdateMentorAsync(string id, MentorDTO updatedMentor)
        {
            var existing = _mentors.FirstOrDefault(m => m.Id == id);
            if (existing is not null) 
            {
                existing.Id = updatedMentor.Id;
                existing.Name = updatedMentor.Name;
                existing.Email = updatedMentor.Email;
                existing.PhoneNumber = updatedMentor.PhoneNumber;
                existing.Teams = updatedMentor.TeamIds.Select(_teamService.GetTeamModelById).OfType<Team>().ToList();
                existing.Projects = updatedMentor.ProjectIds.Select(_projectService.GetProjectModelById).OfType<Project>().ToList();
                return updatedMentor;
            }
            throw new MentorNotFoundException($"Mentor with ID {id} not found.");
        }

        /// <summary>
        /// Deletes a mentor with the specified ID. If a mentor with the given ID is found, it is removed from the list of mentors and the method returns true. If no mentor is found with the specified ID, a MentorNotFoundException is thrown with an appropriate message and the method returns false.
        /// </summary>
        /// <param name="id">The ID of the mentor to delete.</param>
        /// <returns>A boolean indicating whether the mentor was deleted.</returns>
        /// <exception cref="MentorNotFoundException"></exception>
        public async Task<bool> DeleteMentorAsync(string id)
        {
            var existing = _mentors.FirstOrDefault(m => m.Id == id);
            if (existing is not null) 
            {
                _mentors.Remove(existing);
                return true;
            }
            throw new MentorNotFoundException($"Mentor with ID {id} not found.");
            return false;
        }
    }
}

/// <summary>
/// Custom exception class to indicate that a mentor was not found in the database. This exception is thrown when an operation attempts to access a mentor that does not exist, such as retrieving, updating, or deleting a mentor by its ID. The exception message provides details about the specific mentor that was not found, allowing for better error handling and debugging in the application.
/// </summary>
public class MentorNotFoundException : Exception
{
    public MentorNotFoundException(string message) : base(message)
    {
        
    }
}

/// <summary>
/// Custom exception class to indicate that a mentor with the same attributes already exists in the database. This exception is thrown when an attempt is made to add a new mentor that has the same ID as an existing mentor, or when the provided mentor data conflicts with existing mentors in a way that violates uniqueness constraints. The exception message provides details about the specific conflict, allowing for better error handling and debugging in the application.
/// </summary>
public class MentorAlreadyExistsException : Exception
{
    public MentorAlreadyExistsException(string message) : base(message)
    {
        
    }
}
