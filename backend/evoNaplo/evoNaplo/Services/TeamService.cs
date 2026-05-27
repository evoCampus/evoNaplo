using evoNaplo.DTO;
using evoNaplo.Models;
using evoNaplo.Exceptions;

namespace evoNaplo.Services;

/// <summary>
/// The TeamService class provides methods for managing teams in the application. It allows for retrieving, adding, updating, and deleting teams. The service uses an in-memory list to store team data and interacts with mentor and student services to manage relationships between teams, mentors, and students. The service also includes error handling to ensure that appropriate exceptions are thrown when operations fail, such as when a team is not found or when trying to add a team that already exists.
/// </summary>
internal class TeamService : ITeamService
{
    private static readonly List<Team> _teams = new List<Team>();
    private readonly IMentorService _mentorService;
    private readonly IStudentService _studentService;

    public TeamService(/*IMentorService mentorService,*/ IStudentService studentService)
    {
        //_mentorService = mentorService;
        _studentService = studentService;
    }

    /// <summary>
    /// Retrieves a team model by its ID. If a team with the specified ID is found in the list of teams, it is returned. If no team is found with the given ID, a TeamNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the team to retrieve.</param>
    /// <returns>The Team model if found.</returns>
    /// <exception cref="TeamNotFoundException"></exception>
    public Team GetTeamModelById(string id)
    {
        var team = _teams.FirstOrDefault(t => t.Id == id);
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
        IEnumerable<TeamDTO> teams = _teams.Select(t => new TeamDTO(t));
        return teams;
    }

    /// <summary>
    /// Retrieves a team by its ID and returns it as a TeamDTO. If a team with the specified ID is found in the list of teams, it is converted into a TeamDTO and returned. If no team is found with the given ID, a TeamNotFoundException is thrown with an appropriate message.
    /// </summary>
    /// <param name="id">The ID of the team to retrieve.</param>
    /// <returns>The TeamDTO if found.</returns>
    /// <exception cref="TeamNotFoundException"></exception>
    public async Task<TeamDTO> GetTeamByIdAsync(string id)
    {
        Team? team = _teams.FirstOrDefault(t => t.Id == id);
        if (team is not null) 
        {
            return new TeamDTO(team);
        }
        throw new TeamNotFoundException($"Team with ID {id} not found.");
    }

    /// <summary>
    /// Adds a new team to the list of teams. The method takes a TeamDTO as input, generates a new unique ID for the team, and creates a new Team model based on the provided DTO. The new team is then added to the list of teams, and the original TeamDTO (with the newly assigned ID) is returned. This allows for the creation of new team entries in the application while ensuring that each team has a unique identifier.
    /// </summary>
    /// <param name="teamToAdd">The TeamDTO to add.</param>
    /// <returns>The added TeamDTO if successful.</returns>
    public async Task<TeamDTO> AddTeamAsync(TeamDTO teamToAdd)
    {
        teamToAdd.Id = Guid.NewGuid().ToString();
        _teams.Add(new Team
        {
            Id = teamToAdd.Id,
            Mentors = teamToAdd.MentorIds.Select(_mentorService.GetMentorModelById).OfType<Mentor>().ToList(),
            Students = teamToAdd.StudentIds.Select(_studentService.GetStudentModelById).OfType<Student>().ToList(),
            //WeeklyMeetingDay = teamToAdd.WeeklyMeetingDay,
            //WeeklyMeetingTime = teamToAdd.WeeklyMeetingTime,
            AttendanceSheets = teamToAdd.AttendanceSheetIds.Select(a => new AttendanceSheet { Id = a }).ToList()
        });
        return teamToAdd;
    }

    /// <summary>
    /// Updates an existing team with the specified ID using the provided TeamDTO. The method first checks if a team with the given ID exists in the list of teams. If a team is found, its properties are updated with the values from the provided TeamDTO, and the updated TeamDTO is returned. If no team is found with the specified ID, a TeamNotFoundException is thrown with an appropriate message. This allows for modifying existing team entries in the application while ensuring that only valid teams can be updated.
    /// </summary>
    /// <param name="id">The ID of the team to update.</param>
    /// <param name="updatedTeam">The updated team DTO.</param>
    /// <returns>The updated TeamDTO if successful.</returns>
    /// <exception cref="TeamNotFoundException"></exception>
    public async Task<TeamDTO> UpdateTeamAsync(string id, TeamDTO updatedTeam)
    {
        var existing = _teams.FirstOrDefault(t => t.Id == id);
        if (existing is not null) 
        {
            existing.Id = updatedTeam.Id;
            existing.Mentors = updatedTeam.MentorIds.Select(_mentorService.GetMentorModelById).OfType<Mentor>().ToList();
            existing.Students = updatedTeam.StudentIds.Select(_studentService.GetStudentModelById).OfType<Student>().ToList();
            //existing.WeeklyMeetingDay = updatedTeam.WeeklyMeetingDay;
            //existing.WeeklyMeetingTime = updatedTeam.WeeklyMeetingTime;
            existing.AttendanceSheets = updatedTeam.AttendanceSheetIds.Select(a => new AttendanceSheet { Id = a }).ToList();
            return updatedTeam;
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
        var existing = _teams.FirstOrDefault(t => t.Id == id);
        if (existing is not null) 
        {
            _teams.Remove(existing);
            return true;
        }
        throw new TeamNotFoundException($"Team with ID {id} not found.");
    }
    
}
