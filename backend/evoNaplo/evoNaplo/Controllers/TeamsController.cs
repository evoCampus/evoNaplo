using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private static List<TeamDTO> _teams = new List<TeamDTO>();
    private static long _nextId = 1;

    // GET: api/teams
    [HttpGet]
    public ActionResult<IEnumerable<TeamDTO>> GetTeams()
    {
        return Ok(_teams);
    }

    // GET: api/teams/5
    [HttpGet("{id}")]
    public ActionResult<TeamDTO> GetTeam(long id)
    {
        var team = _teams.FirstOrDefault(s => s.Id == id);
        if (team == null)
            return NotFound();
        return Ok(team);
    }

    // POST: api/teams
    [HttpPost]
    public ActionResult<TeamDTO> CreateTeam(TeamDTO team)
    {
        team.Id = _nextId++;
        _teams.Add(team);
        return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
    }

    // PUT: api/teams/5
    [HttpPut("{id}")]
    public ActionResult UpdateTeam(long id, TeamDTO updatedTeam)
    {
        var index = _teams.FindIndex(s => s.Id == id);
        if (index == -1)
            return NotFound();
        _teams[index] = updatedTeam;
        return NoContent();
    }

    // DELETE: api/teams/5
    [HttpDelete("{id}")]
    public ActionResult DeleteTeam(long id)
    {
        var team = _teams.FirstOrDefault(s => s.Id == id);
        if (team == null)
            return NotFound();
        _teams.Remove(team);
        return NoContent();
    }
}
