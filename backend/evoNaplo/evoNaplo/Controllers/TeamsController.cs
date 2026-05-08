using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    /// <summary>
    /// Retrieves a list of all teams in the database. If no teams are found, a NotFound response is returned with an appropriate message. Each team is returned as a TeamDTO.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="TeamDTO"/> objects representing all teams in the database.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
    {
        try
        {
            return Ok(await _teamService.GetAllTeamsAsync());
        }
        catch (TeamNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a specific team by its ID. If a team with the specified ID is found, it is returned as a TeamDTO. If no team is found with the given ID, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <param name="teamId">The ID of the team to retrieve.</param>
    /// <returns>The TeamDTO if found, otherwise a NotFound response.</returns>
    [HttpGet("{teamId}")]
    public async Task<ActionResult<TeamDTO>> GetTeam(string teamId)
    {
        try
        {
            return Ok(await _teamService.GetTeamByIdAsync(teamId));
        }
        catch (TeamNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Adds a new team to the database. If a team with the same name already exists, a Conflict response is returned with an appropriate message. If the team is added successfully, the created TeamDTO is returned in the response body.
    /// </summary>
    /// <param name="teamToCreate">The team data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created team.</returns>
    [HttpPost]
    public async Task<ActionResult<TeamDTO>> CreateTeam(TeamDTO teamToCreate)
    {
        try
        {
            return Ok(await _teamService.AddTeamAsync(teamToCreate));
        }
        catch (TeamAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing team in the database based on the provided identifier and the provided updated team data. If no team with the given identifier exists, a NotFound response is returned with an appropriate message. If the team is updated successfully, a NoContent response is returned.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to update. Cannot be null or empty.</param>
    /// <param name="updatedTeam">An object containing the updated team information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the team is not found.</returns>
    [HttpPut("{teamId}")]
    public async Task<ActionResult> UpdateTeam(string teamId, TeamDTO updatedTeam)
    {
        try
        {
            await _teamService.UpdateTeamAsync(teamId, updatedTeam);
            return NoContent();
        }
        catch (TeamNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a team from the database based on the provided identifier. If no team with the given identifier exists, a NotFound response is returned with an appropriate message. If the team is deleted successfully, a NoContent response is returned.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the deletion is successful; otherwise, an HTTP 404 response if the team is not found.</returns>
    [HttpDelete("{teamId}")]
    public async Task<ActionResult> DeleteTeam(string teamId)
    {
        try
        {
            await _teamService.DeleteTeamAsync(teamId);
            return NoContent();
        }
        catch (TeamNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
