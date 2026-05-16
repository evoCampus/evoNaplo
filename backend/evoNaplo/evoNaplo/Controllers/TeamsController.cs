namespace evoNaplo.Controllers;

/// <summary>
/// Controller for managing teams in the application. Provides endpoints for retrieving, creating, updating, and deleting team records. Each endpoint interacts with the ITeamService to perform the necessary operations on the team data. The controller handles exceptions such as TeamNotFoundException and TeamAlreadyExistsException to return appropriate HTTP responses based on the outcome of each operation.
/// </summary>
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
    /// Retrieves a list of all teams. If teams are found, they are returned as a list of TeamDTO objects. If no teams are found, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <returns>A list of TeamDTO objects if teams are found; otherwise, a NotFound response.</returns>
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
    /// Retrieves a specific team by its unique identifier. If a team with the given identifier exists, it is returned as a TeamDTO object. If no team is found with the provided identifier, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to retrieve.</param>
    /// <returns>The TeamDTO if found; otherwise, a NotFound response.</returns>
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
    /// Creates a new team in the database based on the provided TeamDTO object. If a team with the same unique identifier already exists, a Conflict response is returned with an appropriate message. If the team is created successfully, the newly created TeamDTO object is returned in the response.
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
    /// Updates an existing team in the database based on the provided identifier and updated TeamDTO object. If no team with the given identifier exists, a NotFound response is returned with an appropriate message. If the team is updated successfully, the updated TeamDTO object is returned in the response.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to update. Cannot be null or empty.</param>
    /// <param name="updatedTeam">An object containing the updated team information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated team.</returns>
    [HttpPut("{teamId}")]
    public async Task<ActionResult> UpdateTeam(string teamId, TeamDTO updatedTeam)
    {
        try
        {
            return Ok(await _teamService.UpdateTeamAsync(teamId, updatedTeam));
        }
        catch (TeamNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a team from the database based on the provided identifier. If no team with the given identifier exists, a NotFound response is returned with an appropriate message. If the team is deleted successfully, an Ok response is returned indicating the successful deletion.
    /// </summary>
    /// <param name="teamId">The unique identifier of the team to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 200 response if the deletion is successful; otherwise, an HTTP 404 response if the team is not found.</returns>
    [HttpDelete("{teamId}")]
    public async Task<ActionResult> DeleteTeam(string teamId)
    {
        try
        {
            return Ok(await _teamService.DeleteTeamAsync(teamId));
        }
        catch (TeamNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
}
