using evoNaplo.DAL.Interfaces;
using evoNaplo.DTO.MentorDTOs;
using evoNaplo.DTO.TeamDTOs;
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
        return teams?.Select(t => new TeamDTO(t)) ?? Enumerable.Empty<TeamDTO>();
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
    public async Task<TeamDTO> AddTeamAsync(CreateTeamDTO teamToAddDTO)
    {
        var newTeam = new Team
        {
            Id = string.Empty,
            Mentors = new List<Mentor>(),
            Students = new List<Student>(),
            AttendanceSheets = new List<AttendanceSheet>(),
            WeeklyMeetingDay = teamToAddDTO.WeeklyMeetingDay,
            WeeklyMeetingTime = teamToAddDTO.WeeklyMeetingTime
        };

        if (!string.IsNullOrEmpty(teamToAddDTO.MentorId))
        {
            var mentor = await _mentorRepository.GetMentorByIdAsync(teamToAddDTO.MentorId);
            if (mentor is not null)
            {
                newTeam.Mentors.Add(mentor);
            }
        }

        if (!string.IsNullOrEmpty(teamToAddDTO.StudentId))
        {
            var student = await _studentRepository.GetStudentByIdAsync(teamToAddDTO.StudentId);
            if (student is not null)
            {
                newTeam.Students.Add(student);
            }
        }

        if (!string.IsNullOrEmpty(teamToAddDTO.AttendanceSheetId))
        {
            newTeam.AttendanceSheets.Add(new AttendanceSheet
            { 
                Id = teamToAddDTO.AttendanceSheetId
            });
        }

        await _teamRepository.AddTeamAsync(newTeam);
        return new TeamDTO(newTeam);
    }

    /// <summary>
    /// Updates an existing team with the specified ID using the provided TeamDTO. The method first checks if a team with the given ID exists in the list of teams. If a team is found, its properties are updated with the values from the provided TeamDTO, and the updated TeamDTO is returned. If no team is found with the specified ID, a TeamNotFoundException is thrown with an appropriate message. This allows for modifying existing team entries in the application while ensuring that only valid teams can be updated.
    /// </summary>
    /// <param name="id">The ID of the team to update.</param>
    /// <param name="updatedTeamDTO">The updated team DTO.</param>
    /// <returns>The updated TeamDTO if successful.</returns>
    /// <exception cref="TeamNotFoundException"></exception>
    public async Task<TeamDTO> UpdateTeamAsync(string id, UpdateTeamDTO updatedTeamDTO)
    {
        var existingTeam = await _teamRepository.GetTeamByIdAsync(id);

        if (existingTeam is not null)
        {
            existingTeam.Mentors ??= new List<Mentor>();
            existingTeam.Students ??= new List<Student>();
            existingTeam.AttendanceSheets ??= new List<AttendanceSheet>();
            existingTeam.WeeklyMeetingDay = updatedTeamDTO.WeeklyMeetingDay;
            existingTeam.WeeklyMeetingTime = updatedTeamDTO.WeeklyMeetingTime;

            existingTeam.Mentors.Clear();
            if (!string.IsNullOrEmpty(updatedTeamDTO.MentorId))
            {
                var mentor = await _mentorRepository.GetMentorByIdAsync(updatedTeamDTO.MentorId);
                if (mentor is not null)
                {
                    existingTeam.Mentors.Add(mentor);
                }
            }

            existingTeam.Students.Clear();
            if (!string.IsNullOrEmpty(updatedTeamDTO.StudentId))
            {
                var student = await _studentRepository.GetStudentByIdAsync(updatedTeamDTO.StudentId);
                if (student is not null)
                {
                    existingTeam.Students.Add(student);
                }
            }

            existingTeam.AttendanceSheets.Clear();
            if (!string.IsNullOrEmpty(updatedTeamDTO.AttendanceSheetId))
            {
                existingTeam.AttendanceSheets.Add(new AttendanceSheet
                {
                    Id = updatedTeamDTO.AttendanceSheetId,
                    TeamId = existingTeam.Id
                });
            }

            await _teamRepository.UpdateTeamAsync(existingTeam);
            
            return new TeamDTO(existingTeam);
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
        await _teamRepository.DeleteTeamAsync(id);
        return true;
    }
}
