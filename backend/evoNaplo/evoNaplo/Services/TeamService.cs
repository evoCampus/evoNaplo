using evoNaplo.DAL.Interfaces;
using evoNaplo.DAL.Repositories;
using evoNaplo.DTO;
using evoNaplo.Exceptions;
using evoNaplo.Models;

namespace evoNaplo.Services;

/// <summary>
/// The TeamService class provides methods for managing teams in the application. It allows for retrieving, adding, updating, and deleting teams. The service uses an in-memory list to store team data and interacts with mentor and student services to manage relationships between teams, mentors, and students. The service also includes error handling to ensure that appropriate exceptions are thrown when operations fail, such as when a team is not found or when trying to add a team that already exists.
/// </summary>
internal class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMentorRepository _mentorRepository;
    private readonly IStudentRepository _studentRepository;

    public TeamService(IMentorRepository mentorRepository, IStudentRepository studentRepository, ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
        _mentorRepository = mentorRepository;
        _studentRepository = studentRepository;
    }

    /// <summary>
    /// Retrieves a team model by its ID. If a team with the specified ID is found in the list of teams, it is returned. If no team is found with the given ID, a TeamNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the team to retrieve.</param>
    /// <returns>The Team model if found.</returns>
    /// <exception cref="TeamNotFoundException"></exception>
    public async Task<Team> GetTeamModelById(string id)
    {
        var team = await _teamRepository.GetTeamByIdAsync(id);
        if (team is null)
        {
            throw new TeamNotFoundException($"Team with ID {id} not found.");
        }
        return team;
    }

    /// <summary>
    /// Retrieves all teams as a collection of TeamDTOs. The method iterates through the list of teams and converts each team model into a TeamDTO, which is then returned as an IEnumerable collection. This allows for a more structured and simplified representation of team data when it is accessed by other parts of the application.
    /// </summary>
    /// <returns>An IEnumerable collection of TeamDTOs representing all teams.</returns>
    public async Task<IEnumerable<TeamDTO>> GetAllTeamsAsync()
    {
        var teams = await _teamRepository.GetAllTeamsAsync();
        return teams.Select(t => new TeamDTO(t));
    }

    /// <summary>
    /// Retrieves a team by its ID and returns it as a TeamDTO. If a team with the specified ID is found in the list of teams, it is converted into a TeamDTO and returned. If no team is found with the given ID, a TeamNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the team to retrieve.</param>
    /// <returns>The TeamDTO if found.</returns>
    /// <exception cref="TeamNotFoundException"></exception>
    public async Task<TeamDTO> GetTeamByIdAsync(string id)
    {
        var team = await _teamRepository.GetTeamByIdAsync(id);
        if (team is not null)
        {
            return new TeamDTO(team);
        }
        throw new TeamNotFoundException($"Team with ID {id} not found.");
    }

    /// <summary>
    /// Adds a new team to the list of teams. The method takes a TeamDTO as input, generates a new unique ID for the team, and creates a new Team model based on the provided DTO. The new team is then added to the list of teams, and the original TeamDTO (with the newly assigned ID) is returned. This allows for the creation of new team entries in the application while ensuring that each team has a unique identifier.
    /// </summary>
    /// <param name="teamToAddDTO">The TeamDTO to add.</param>
    /// <returns>The added TeamDTO if successful.</returns>
    public async Task<TeamDTO> AddTeamAsync(TeamDTO teamToAddDTO)
    {
        // id will be generated on other branch
        var newId = string.IsNullOrWhiteSpace(teamToAddDTO.Id) ? Guid.NewGuid().ToString() : teamToAddDTO.Id;

        var newTeam = new Team
        {
            Id = newId,
            Mentors = new List<Mentor>(),
            Students = new List<Student>(),
            AttendanceSheets = new List<AttendanceSheet>()
        };

        if (teamToAddDTO.MentorIds is not null)
        {
            foreach (var mentorId in teamToAddDTO.MentorIds)
            {
                var mentor = await _mentorRepository.GetMentorByIdAsync(mentorId);
                if (mentor is not null)
                {
                    newTeam.Mentors.Add(mentor);
                }
            }
        }

        if (teamToAddDTO.StudentIds is not null)
        {
            foreach (var studentId in teamToAddDTO.StudentIds)
            {
                var student = await _studentRepository.GetStudentByIdAsync(studentId);
                if (student is not null)
                {
                    newTeam.Students.Add(student);
                }
            }
        }

        if (teamToAddDTO.AttendanceSheetIds is not null)
        {
            foreach (var sheetId in teamToAddDTO.AttendanceSheetIds)
            {
                newTeam.AttendanceSheets.Add(new AttendanceSheet
                {
                    Id = sheetId,
                    TeamId = newId
                });
            }
        }

        await _teamRepository.AddTeamAsync(newTeam);
        teamToAddDTO.Id = newTeam.Id;
        return teamToAddDTO;
    }

    /// <summary>
    /// Updates an existing team with the specified ID using the provided TeamDTO. The method first checks if a team with the given ID exists in the list of teams. If a team is found, its properties are updated with the values from the provided TeamDTO, and the updated TeamDTO is returned. If no team is found with the specified ID, a TeamNotFoundException is thrown with an appropriate message. This allows for modifying existing team entries in the application while ensuring that only valid teams can be updated.
    /// </summary>
    /// <param name="id">The ID of the team to update.</param>
    /// <param name="updatedTeamDTO">The updated team DTO.</param>
    /// <returns>The updated TeamDTO if successful.</returns>
    /// <exception cref="TeamNotFoundException"></exception>
    public async Task<TeamDTO> UpdateTeamAsync(string id, TeamDTO updatedTeamDTO)
    {
        var existingTeam = await _teamRepository.GetTeamByIdAsync(id);

        if (existingTeam is not null)
        {
            if (updatedTeamDTO.MentorIds is not null)
            {
                foreach (var mentorId in updatedTeamDTO.MentorIds)
                {
                    if (!existingTeam.Mentors.Any(m => m.Id == mentorId))
                    {
                        var mentor = await _mentorRepository.GetMentorByIdAsync(mentorId);
                        if (mentor is not null) existingTeam.Mentors.Add(mentor);
                    }
                }
            }

        if (updatedTeamDTO.StudentIds is not null)
            {
            foreach (var studentId in updatedTeamDTO.StudentIds)
            {
                if (!existingTeam.Students.Any(s => s.Id == studentId))
                {
                    var student = await _studentRepository.GetStudentByIdAsync(studentId);
                    if (student is not null) existingTeam.Students.Add(student);
                }
            }
        }

        if (updatedTeamDTO.AttendanceSheetIds is not null)
        {
            foreach (var sheetId in updatedTeamDTO.AttendanceSheetIds)
            {
                if (!existingTeam.AttendanceSheets.Any(a => a.Id == sheetId))
                {
                    existingTeam.AttendanceSheets.Add(new AttendanceSheet
                    {
                        Id = sheetId,
                        TeamId = existingTeam.Id
                    });
                }
            }
        }
            await _teamRepository.UpdateTeamAsync(existingTeam);
            updatedTeamDTO.Id = existingTeam.Id;
            return updatedTeamDTO;
        }
        throw new TeamNotFoundException($"Team with ID {id} not found.");
    } 
    
    /// <summary>
    /// Deletes a team with the specified ID from the list of teams. The method checks if a team with the given ID exists in the list. If a team is found, it is removed from the list, and the method returns true to indicate that the deletion was successful. If no team is found with the specified ID, a TeamNotFoundException is thrown with an appropriate message. This allows for the removal of team entries from the application while ensuring that only valid teams can be deleted.
    /// </summary>
    /// <param name="id">The ID of the team to delete.</param>
    /// <returns>A boolean indicating whether the team was deleted.</returns>
    /// <exception cref="TeamNotFoundException"></exception>
    public async Task<bool> DeleteTeamAsync(string id)
    {
        var existingTeam = await _teamRepository.GetTeamByIdAsync(id);
        if (existingTeam is not null) 
        {
            await _teamRepository.DeleteTeamAsync(id);
            return true;
        }
        throw new TeamNotFoundException($"Team with ID {id} not found.");
    }
}
