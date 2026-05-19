using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private static List<TeamDTO> _teams = new List<TeamDTO>();

    /// <summary>
    /// Retrieves all teams as data transfer objects.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see
    /// cref="TeamDTO"/> objects representing all teams. The collection is empty if no teams are available.</returns>
    [HttpGet]
    public Task<IEnumerable<TeamDTO>> GetTeams()
    {
        return Task.FromResult((IEnumerable<TeamDTO>)_teams);
    }

    /// <summary>
    /// Retrieves the team with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the team to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="TeamDTO"/> representing
    /// the requested team.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a team with the specified <paramref name="id"/> does not exist.</exception>
    [HttpGet("{id}")]
    public Task<TeamDTO> GetTeam(string id)
    {
        var team = _teams.FirstOrDefault(s => s.Id == id);
        if (team == null)
            throw new KeyNotFoundException($"Team with id {id} not found.");
        return Task.FromResult<TeamDTO>(team);
    }

    /// <summary>
    /// Creates a new team and adds it to the collection.
    /// </summary>
    /// <param name="team">The team to add. Must not be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created team.</returns>
    [HttpPost]
    public Task<TeamDTO> CreateTeam(TeamDTO team)
    {
        _teams.Add(team);
        return Task.FromResult<TeamDTO>(team);
    }

    /// <summary>
    /// Updates the details of an existing team with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the team to update. Cannot be null or empty.</param>
    /// <param name="updatedTeam">An object containing the updated team information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update
    /// is successful; otherwise, an HTTP 404 response if the team is not found.</returns>
    [HttpPut("{id}")]
    public Task UpdateTeam(string id, TeamDTO updatedTeam)
    {
        var index = _teams.FindIndex(s => s.Id == id);
        if (index == -1)
            return Task.FromResult(NotFound());
        _teams[index] = updatedTeam;
        return Task.FromResult(NoContent());
    }

    /// <summary>
    /// Deletes the team with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the team to delete. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous delete operation. The task result contains an HTTP 204 No Content
    /// response if the deletion is successful.</returns>
    [HttpDelete("{id}")]
    public Task DeleteTeam(string id)
    {
        var team = _teams.FirstOrDefault(s => s.Id == id);
        _teams.Remove(team);
        return Task.FromResult(NoContent());
    }
}
