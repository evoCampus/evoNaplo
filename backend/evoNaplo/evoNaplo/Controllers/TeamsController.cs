using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;
using evoNaplo.Models;

[ApiController]
[Route("api/[controller]")]
internal class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly IMentorService _mentorService;
    private readonly IStudentService _studentService;

    public TeamsController(ITeamService teamService, IMentorService mentorService, IStudentService studentService)
    {
        _teamService = teamService;
        _mentorService = mentorService;
        _studentService = studentService;
    }

    /// <summary>
    /// Retrieves a list of all teams in the system, returning their details as TeamDTO objects. If no teams are found, an empty collection is returned.
     /// </summary>
     /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of team data transfer objects.</returns>
    [HttpGet]
    public Task<IEnumerable<TeamDTO>> GetTeams()
    {
        return Task.FromResult(_teamService.GetAllTeams().Select(team => new TeamDTO
        {
            Id = team.Id,
            MentorIds = team.Mentors?.Select(mentor => mentor.Id) ?? Enumerable.Empty<string>(),
            StudentIds = team.Students?.Select(student => student.Id) ?? Enumerable.Empty<string>(),
            AttendanceSheetIds = team.AttendanceSheets?.Select(sheet => sheet.Id) ?? Enumerable.Empty<string>()
        }));
    }

    /// <summary>
    /// Retrieves the details of a specific team based on the provided identifier. If no team with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="TeamDTO"/> representing
    /// the requested team.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDTO>> GetTeam(string teamId)
    {
        Team? team = _teamService.GetTeamById(teamId);
        if (team is null)
            return NotFound($"Team with id {teamId} not found.");
        return Ok(new TeamDTO
        {
            Id = team.Id,
            MentorIds = team.Mentors?.Select(mentor => mentor.Id) ?? Enumerable.Empty<string>(),
            StudentIds = team.Students?.Select(student => student.Id) ?? Enumerable.Empty<string>(),
            AttendanceSheetIds = team.AttendanceSheets?.Select(sheet => sheet.Id) ?? Enumerable.Empty<string>()
        });
    }

    /// <summary>
    /// Creates a new team in the system based on the provided team data. If a team with the same identifier already exists, a Conflict response is returned.
    /// </summary>
    /// <param name="teamToCreate">The data for the team to create.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created team.</returns>
    [HttpPost]
    public async Task<ActionResult<Team>> CreateTeam(TeamDTO teamToCreate)
    {
        if (_teamService.GetTeamById(teamToCreate.Id) is not null)
            return Conflict($"Team with ID {teamToCreate.Id} already exists.");
        Team newTeam = new Team
        {
            Id = teamToCreate.Id,
            Mentors = teamToCreate.MentorIds?.Select(id => _mentorService.GetMentorById(id)).OfType<Mentor>().ToList() ?? new List<Mentor>(),
            Students = teamToCreate.StudentIds?.Select(id => _studentService.GetStudentById(id)).OfType<Student>().ToList() ?? new List<Student>(),
        };
        _teamService.AddTeam(newTeam);
        return Ok(newTeam);
    }

    /// <summary>
    /// Updates the details of an existing team in the system based on the provided identifier and updated team data. If no team with the given identifier exists, a NotFound response is returned. Upon successful update, an HTTP 204 No Content response is returned.
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
            Mentors = updatedTeam.MentorIds?.Select(id => _mentorService.GetMentorById(id)).OfType<Mentor>().ToList() ?? new List<Mentor>(),
            Students = updatedTeam.StudentIds?.Select(id => _studentService.GetStudentById(id)).OfType<Student>().ToList() ?? new List<Student>(),
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
