namespace evoNaplo.Services;

internal class TeamService : ITeamService
{
    private static readonly List<Team> _teams = new List<Team>();
    private readonly IMentorService _mentorService;
    private readonly IStudentService _studentService;

    public TeamService(IMentorService mentorService, IStudentService studentService)
    {
        _mentorService = mentorService;
        _studentService = studentService;
    }

    /// <summary>
    /// This method is used internally by the service to retrieve the Team model for operations that require it, such as adding or updating teams. It is not intended to be exposed directly to clients, as it returns the internal model rather than a DTO.
    /// </summary>
    /// <param name="id">The ID of the team to retrieve.</param>
    /// <returns>The Team model if found, otherwise null.</returns>
    public Team? GetTeamModelById(string id)
    {
        return _teams.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Retrieves a list of all teams in the database. If no teams are found, a TeamNotFoundException is thrown with an appropriate message. Each team is returned as a TeamDTO.
    /// </summary>
    /// <returns>A list of TeamDTOs if teams are found.</returns>
    /// <exception cref="TeamNotFoundException"></exception>
    public async Task<IEnumerable<TeamDTO>> GetAllTeamsAsync()
    {
        IEnumerable<TeamDTO> teams = _teams.Select(t => new TeamDTO(t));
        if (teams.Any())
        {
            return teams;
        }
        throw new TeamNotFoundException("No teams found.");
    }

    /// <summary>
    /// Retrieves a specific team by its ID and returns it as a DTO, including related mentor and student IDs, and attendance sheet IDs.
    /// </summary>
    /// <param name="id">The ID of the team to retrieve.</param>
    /// <returns>The team DTO if found.</returns>
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
    /// Adds a new team to the list. If a team with the same ID already exists, a TeamAlreadyExistsException is thrown with an appropriate message. If the team is added successfully, the provided TeamDTO is returned. If the ID of the team to add is null or empty, a new GUID will be generated and assigned as the ID.
    /// </summary>
    /// <param name="teamToAdd">The TeamDTO to add.</param>
    /// <returns>The added TeamDTO if successful.</returns>
    /// <exception cref="TeamAlreadyExistsException"></exception>
    public async Task<TeamDTO> AddTeamAsync(TeamDTO teamToAdd)
    {
        if (string.IsNullOrEmpty(teamToAdd.Id)) 
        {
            teamToAdd.Id = Guid.NewGuid().ToString();
        }
        if (_teams.Any(t => t.Id == teamToAdd.Id))
        {
            throw new TeamAlreadyExistsException($"A team with the ID {teamToAdd.Id} already exists.");
        }
        _teams.Add(new Team
        {
            Id = teamToAdd.Id,
            Mentors = teamToAdd.MentorIds.Select(_mentorService.GetMentorModelById).OfType<Mentor>().ToList(),
            Students = teamToAdd.StudentIds.Select(_studentService.GetStudentModelById).OfType<Student>().ToList(),
            //WeeklyMeetingDay = teamToAdd.WeeklyMeetingDay,
            //WeeklyMeetingTime = teamToAdd.WeeklyMeetingTime,
            AttendanceSheets = teamToAdd.AttendanceSheetIds.Select(a => new AttendanceSheet { Id = a }).ToList(),
        });
        return teamToAdd;
    }

    /// <summary>
    /// Updates an existing team with the specified ID using the provided updated team data. If a team with the given ID is found, it is updated with the new data and the updated TeamDTO is returned. If no team is found with the specified ID, a TeamNotFoundException is thrown with an appropriate message.
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
    /// Deletes a team with the specified ID. If a team with the given ID is found, it is removed from the list of teams and the method returns true. If no team is found with the specified ID, a TeamNotFoundException is thrown with an appropriate message and the method returns false.
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
        return false;
    }
    
}
