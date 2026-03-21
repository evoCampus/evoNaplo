using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private static List<TeamDTO> _teams = new List<TeamDTO>();
    private static long _nextId = 1;

    /// <summary>
    /// Retrieves all teams as data transfer objects.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of team
    /// data transfer objects. The collection is empty if no teams are available.</returns>
    [HttpGet]
    public Task<IEnumerable<TeamDTO>> GetTeams()
    {
        return Task.FromResult((IEnumerable<TeamDTO>)_teams);
    }

    /// <summary>
    /// Retrieves the team with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the team to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ActionResult{TeamDTO}"/> with the team data if found; otherwise, a NotFound result.</returns>
    [HttpGet("{id}")]
    public Task<ActionResult<TeamDTO>> GetTeam(long id)
    {
        var team = _teams.FirstOrDefault(s => s.Id == id);
        if (team == null)
            return Task.FromResult<ActionResult<TeamDTO>>(NotFound());
        return Task.FromResult<ActionResult<TeamDTO>>(Ok(team));
    }

    /// <summary>
    /// Creates a new team and adds it to the collection.
    /// </summary>
    /// <param name="team">The team data to create. Must not be null. The team identifier is assigned by the server.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ActionResult that includes the
    /// created team data and a location header for retrieving the team by its identifier.</returns>
    [HttpPost]
    public Task<ActionResult<TeamDTO>> CreateTeam(TeamDTO team)
    {
        team.Id = _nextId++;
        _teams.Add(team);
        return Task.FromResult<ActionResult<TeamDTO>>(CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team));
    }

    /// <summary>
    /// Updates the details of an existing team with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the team to update.</param>
    /// <param name="updatedTeam">An object containing the updated team information to apply.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that
    /// is <see cref="NotFoundResult"/> if the team does not exist, or <see cref="NoContentResult"/> if the update is
    /// successful.</returns>
    [HttpPut("{id}")]
    public Task<IActionResult> UpdateTeam(long id, TeamDTO updatedTeam)
    {
        var index = _teams.FindIndex(s => s.Id == id);
        if (index == -1)
            return Task.FromResult<IActionResult>(NotFound());
        _teams[index] = updatedTeam;
        return Task.FromResult<IActionResult>(NoContent());
    }

    /// <summary>
    /// Deletes the team with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the team to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that
    /// is <see cref="NotFoundResult"/> if the team does not exist; otherwise, <see cref="NoContentResult"/> if the
    /// deletion is successful.</returns>
    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteTeam(long id)
    {
        var team = _teams.FirstOrDefault(s => s.Id == id);
        if (team == null)
            return Task.FromResult<IActionResult>(NotFound());
        _teams.Remove(team);
        return Task.FromResult<IActionResult>(NoContent());
    }
}
