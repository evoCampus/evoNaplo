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
        /// Returns the Mentor model by its ID, or null if not found.
        /// </summary>
        /// <param name="id">The ID of the mentor to retrieve.</param>
        /// <returns>The mentor model, or null if not found.</returns>
        public Mentor? GetMentorModelById(string id)
        {
            return _mentors.FirstOrDefault(m => m.Id == id);
        }
        
        /// <summary>
        /// Returns a list of all mentors as MentorDTOs. If a mentor's name, email, or phone number is null, it defaults to "N/A".
        /// </summary>
        /// <returns>A list of MentorDTOs representing all mentors.</returns>
        public IEnumerable<MentorDTO> GetAllMentors()
        {
            return _mentors.Select(mentor => new MentorDTO 
        { 
            Id = mentor.Id, 
            Name = mentor.Name ?? "N/A", 
            Email = mentor.Email ?? "N/A",
            PhoneNumber = mentor.PhoneNumber ?? "N/A",
            TeamIds = mentor.Teams?.Select(team => team.Id).ToList() ?? Enumerable.Empty<string>(),
            ProjectIds = mentor.Projects?.Select(project => project.Id).ToList() ?? Enumerable.Empty<string>(),
        });
        }

        /// <summary>
        /// Returns a MentorDTO for the mentor with the specified ID, or null if not found. If the mentor's name, email, or phone number is null, it defaults to "N/A".
        /// </summary>
        /// <param name="id">The ID of the mentor to retrieve.</param>
        /// <returns>The mentor DTO, or null if not found.</returns>
        public MentorDTO? GetMentorById(string id)
        {
            var mentor = _mentors.FirstOrDefault(m => m.Id == id);
            if (mentor is not null) 
            {
                return new MentorDTO 
                {
                    Id = mentor.Id, 
                    Name = mentor.Name ?? "N/A",
                    Email = mentor.Email ?? "N/A",
                    PhoneNumber = mentor.PhoneNumber ?? "N/A",
                    TeamIds = mentor.Teams?.Select(team => team.Id).ToList() ?? Enumerable.Empty<string>(),
                    ProjectIds = mentor.Projects?.Select(project => project.Id).ToList() ?? Enumerable.Empty<string>(),
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a new mentor to the list. If the mentor's ID is null or empty, a new GUID will be generated for it. The mentor's name, email, and phone number will default to "N/A" if they are null. The mentor's teams and projects will be populated based on the provided team and project IDs, or set to empty lists if the IDs are null.
        /// </summary>
        /// <param name="mentor">The mentor DTO to add.</param>
        public void AddMentor(MentorDTO mentor)
        {
            if (string.IsNullOrEmpty(mentor.Id)) 
            {
                mentor.Id = System.Guid.NewGuid().ToString();
            }
            Mentor newMentor = new Mentor
            {
                Id = mentor.Id,
                Name = mentor.Name ?? "N/A",
                Email = mentor.Email ?? "N/A",
                PhoneNumber = mentor.PhoneNumber ?? "N/A",
                Teams = mentor.TeamIds?.Select(_teamService.GetTeamModelById).OfType<Team>().ToList() ?? new List<Team>(),
                Projects = mentor.ProjectIds?.Select(_projectService.GetProjectModelById).OfType<Project>().ToList() ?? new List<Project>(),
            };
            _mentors.Add(newMentor);
        }

        /// <summary>
        /// Updates an existing mentor with the specified ID using the provided MentorDTO. If the mentor is not found or the updatedMentor is null, the method will return without making any changes. For each property in the updatedMentor that is not null, the corresponding property of the existing mentor will be updated. The mentor's teams and projects will be updated based on the provided team and project IDs, or set to empty lists if the IDs are null.
        /// </summary>
        /// <param name="id">The ID of the mentor to update.</param>
        /// <param name="updatedMentor">The updated mentor DTO.</param>
        public void UpdateMentor(string id, MentorDTO updatedMentor)
        {
            var existing = _mentors.FirstOrDefault(m => m.Id == id);
            if (existing is null || updatedMentor is null) {return;}

            if (updatedMentor.Name is not null) 
            {
                existing.Name = updatedMentor.Name ?? "N/A";
            }
            if (updatedMentor.Email is not null) 
            {
                existing.Email = updatedMentor.Email ?? "N/A";
            }
            if (updatedMentor.PhoneNumber is not null) 
            {
                existing.PhoneNumber = updatedMentor.PhoneNumber ?? "N/A";
            }
            if (updatedMentor.TeamIds is not null) 
            {
                existing.Teams = updatedMentor.TeamIds?.Select(_teamService.GetTeamModelById).OfType<Team>().ToList() ?? new List<Team>();
            }
            if (updatedMentor.ProjectIds is not null) 
            {
                existing.Projects = updatedMentor.ProjectIds?.Select(_projectService.GetProjectModelById).OfType<Project>().ToList() ?? new List<Project>();
            }
        }

        /// <summary>
        /// Deletes the mentor with the specified ID from the list. If the mentor is not found, the method will return without making any changes.
        /// </summary>
        /// <param name="id">The ID of the mentor to delete.</param>
        public void DeleteMentor(string id)
        {
            var existing = _mentors.FirstOrDefault(m => m.Id == id);
            if (existing is not null) 
            {
                _mentors.Remove(existing);
            }
        }
    }
}
