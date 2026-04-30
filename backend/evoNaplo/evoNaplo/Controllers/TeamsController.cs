using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;

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
    /// Retrieves a list of all teams in the system, returning their details as TeamDTO objects. If no teams are found, an empty collection is returned.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of team data transfer objects.</returns>
    [HttpGet]
    public Task<IEnumerable<TeamDTO>> GetTeams()
    {
        return Task.FromResult(_teamService.GetAllTeams());
    }

    /// <summary>
    /// Retrieves the details of a specific team based on the provided identifier. If a team with the given identifier exists, its details are returned as a TeamDTO object. If no team is found with the specified identifier, a NotFound response is returned.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="TeamDTO"/> representing
    /// the requested team.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDTO>> GetTeam(string teamId)
    {
        TeamDTO? team = _teamService.GetTeamById(teamId);
        if (team is null)
            return NotFound($"Team with id {teamId} not found.");
        return Ok(team);
    }

    /// <summary>
    /// Creates a new team in the system based on the provided team data. If a team with the same identifier already exists, a Conflict response is returned.
    /// </summary>
    /// <param name="teamToCreate">The data for the team to create.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created team.</returns>
    [HttpPost]
    public async Task<ActionResult<TeamDTO>> CreateTeam(TeamDTO teamToCreate)
    {
        if (_teamService.GetTeamById(teamToCreate.Id) is null)
        {
            _teamService.AddTeam(teamToCreate);
            return Ok(teamToCreate);
        }
        else
            return Conflict($"Team with ID {teamToCreate.Id} already exists.");
    }

    /// <summary>
    /// Updates the details of an existing team based on the provided identifier and updated team data. If no team with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to update. Cannot be null or empty.</param>
    /// <param name="updatedTeam">An object containing the updated team information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the team is not found.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeam(string teamId, TeamDTO updatedTeam)
    {
        if (_teamService.GetTeamById(teamId) is not null)
        {
            _teamService.UpdateTeam(teamId, updatedTeam);
            return NoContent();
        }
        else
            return NotFound($"Team with ID {teamId} not found.");
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
