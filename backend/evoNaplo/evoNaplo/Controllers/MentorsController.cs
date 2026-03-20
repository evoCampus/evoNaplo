using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class MentorsController : ControllerBase
{
    private static List<MentorDTO> _mentors = new List<MentorDTO>();
    private static long _nextId = 1;

    // GET: api/mentors
    [HttpGet]
    public ActionResult<IEnumerable<MentorDTO>> GetMentors()
    {
        return Ok(_mentors);
    }

    // GET: api/mentors/5
    [HttpGet("{id}")]
    public ActionResult<MentorDTO> GetMentor(long id)
    {
        var mentor = _mentors.FirstOrDefault(s => s.Id == id);
        if (mentor == null)
            return NotFound();
        return Ok(mentor);
    }

    // POST: api/mentors
    [HttpPost]
    public ActionResult<MentorDTO> CreateMentor(MentorDTO mentor)
    {
        mentor.Id = _nextId++;
        _mentors.Add(mentor);
        return CreatedAtAction(nameof(GetMentor), new { id = mentor.Id }, mentor);
    }

    // PUT: api/mentors/5
    [HttpPut("{id}")]
    public ActionResult UpdateMentor(long id, MentorDTO updatedMentor)
    {
        var index = _mentors.FindIndex(s => s.Id == id);
        if (index == -1)
            return NotFound();
        _mentors[index] = updatedMentor;
        return NoContent();
    }

    // DELETE: api/mentors/5
    [HttpDelete("{id}")]
    public ActionResult DeleteMentor(long id)
    {
        var mentor = _mentors.FirstOrDefault(s => s.Id == id);
        if (mentor == null)
            return NotFound();
        _mentors.Remove(mentor);
        return NoContent();
    }
}
