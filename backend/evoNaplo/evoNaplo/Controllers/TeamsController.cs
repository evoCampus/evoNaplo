using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;
using evoNaplo.Models;

[ApiController]
[Route("api/[controller]")]
internal class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    /// <summary>
    /// Retrieves a list of all teams in the system, returning their details as TeamDTO objects.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="TeamDTO"/> objects.</returns>
    [HttpGet]
    public Task<IEnumerable<TeamDTO>> GetTeams()
    {
        return Task.FromResult(_teamService.GetAllTeams().Select(team => new TeamDTO
        {
            Id = team.Id,
            Mentors = team.Mentors ?? Enumerable.Empty<string>(),
            Students = team.Students ?? Enumerable.Empty<string>(),
            WeeklyMeetingDay = team.WeeklyMeetingDay,
            WeeklyMeetingTime = team.WeeklyMeetingTime,
            Attendance = team.Attendance ?? Enumerable.Empty<IEnumerable<string>>()
        }));
    }

    /// <summary>
    /// Retrieves the details of a specific team based on the provided identifier. If no team with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="TeamDTO"/> representing
    /// the requested team.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a team with the specified <paramref name="teamId"/> does not exist.</exception>
    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDTO>> GetTeam(string teamId)
    {
        Team? team = _teamService.GetTeamById(teamId);
        if (team is null)
            return NotFound($"Team with id {teamId} not found.");
        return Ok(new TeamDTO
        {
            Id = team.Id,
            Mentors = team.Mentors ?? Enumerable.Empty<string>(),
            Students = team.Students ?? Enumerable.Empty<string>(),
            WeeklyMeetingDay = team.WeeklyMeetingDay,
            WeeklyMeetingTime = team.WeeklyMeetingTime,
            Attendance = team.Attendance ?? Enumerable.Empty<IEnumerable<string>>()
        });
    }

    /// <summary>
    /// Creates a new team in the system based on the provided team details. If a team with the same identifier already exists, a Conflict response is returned.
    /// </summary>
    /// <param name="teamToCreate">The details of the team to create.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created team.</returns>
    [HttpPost]
    public async Task<ActionResult<Team>> CreateTeam(TeamDTO teamToCreate)
    {
        if (_teamService.GetTeamById(teamToCreate.Id) is not null)
            return Conflict($"Team with ID {teamToCreate.Id} already exists.");
        Team newTeam = new Team
        {
            Id = teamToCreate.Id,
            Mentors = teamToCreate.Mentors ?? new List<string>(),
            Students = teamToCreate.Students ?? new List<string>(),
            WeeklyMeetingDay = teamToCreate.WeeklyMeetingDay,
            WeeklyMeetingTime = teamToCreate.WeeklyMeetingTime,
            Attendance = teamToCreate.Attendance ?? new List<IEnumerable<string>>()
        };
        _teamService.AddTeam(newTeam);
        return Ok(newTeam);
    }

    /// <summary>
    /// Updates the details of an existing team in the system based on the provided identifier and updated team data. If no team with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to update. Cannot be null or empty.</param>
    /// <param name="updatedTeam">An object containing the updated team information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the team is not found.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeam(string teamId, TeamDTO updatedTeam)
    {
        if (_teamService.GetTeamById(teamId) is null)
            return NotFound($"Team with ID {teamId} not found.");
        Team newTeam = new Team
        {
            Id = updatedTeam.Id,
            Mentors = updatedTeam.Mentors ?? new List<string>(),
            Students = updatedTeam.Students ?? new List<string>(),
            WeeklyMeetingDay = updatedTeam.WeeklyMeetingDay,
            WeeklyMeetingTime = updatedTeam.WeeklyMeetingTime,
            Attendance = updatedTeam.Attendance ?? new List<IEnumerable<string>>()
        };
        _teamService.UpdateTeam(teamId, newTeam);
        return NoContent();
    }

    /// <summary>
    /// Deletes an existing team from the system based on the provided identifier. If no team with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 No Content response if the deletion is successful; otherwise, an HTTP 404 Not Found response if the team is not found.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeam(string teamId)
    {
        if (_teamService.GetTeamById(teamId) is null)
            return NotFound($"Team with ID {teamId} not found.");
        _teamService.DeleteTeam(teamId);
        return NoContent();
    }
}
