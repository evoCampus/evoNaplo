using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class MentorsController : ControllerBase
{
    private static List<MentorDTO> _mentors = new List<MentorDTO>();
    private static long _nextId = 1;

    /// <summary>
    /// Retrieves a collection of mentors available in the system.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of mentor
    /// data transfer objects. The collection is empty if no mentors are available.</returns>
    [HttpGet]
    public Task<IEnumerable<MentorDTO>> GetMentors()
    {
        return Task.FromResult((IEnumerable<MentorDTO>)_mentors);
    }

    /// <summary>
    /// Retrieves the mentor with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the mentor to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ActionResult{T}">ActionResult</see> with a <see cref="MentorDTO"/> representing the mentor if found;
    /// otherwise, a NotFound result.</returns>
    [HttpGet("{id}")]
    public Task<ActionResult<MentorDTO>> GetMentor(long id)
    {
        var mentor = _mentors.FirstOrDefault(s => s.Id == id);
        if (mentor == null)
            return Task.FromResult<ActionResult<MentorDTO>>(NotFound());
        return Task.FromResult<ActionResult<MentorDTO>>(Ok(mentor));
    }

    /// <summary>
    /// Creates a new mentor and adds it to the collection.
    /// </summary>
    /// <param name="mentor">The mentor data to create. Must not be null. The Id property is ignored and will be set by the server.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ActionResult that includes the
    /// created mentor data and a location header for retrieving the mentor by Id.</returns>
    [HttpPost]
    public Task<ActionResult<MentorDTO>> CreateMentor(MentorDTO mentor)
    {
        mentor.Id = _nextId++;
        _mentors.Add(mentor);
        return Task.FromResult<ActionResult<MentorDTO>>(CreatedAtAction(nameof(GetMentor), new { id = mentor.Id }, mentor));
    }

    /// <summary>
    /// Updates the details of an existing mentor with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the mentor to update.</param>
    /// <param name="updatedMentor">An object containing the updated mentor information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an IActionResult that is NoContent
    /// if the update is successful, or NotFound if no mentor with the specified identifier exists.</returns>
    [HttpPut("{id}")]
    public Task<IActionResult> UpdateMentor(long id, MentorDTO updatedMentor)
    {
        var index = _mentors.FindIndex(s => s.Id == id);
        if (index == -1)
            return Task.FromResult<IActionResult>(NotFound());
        _mentors[index] = updatedMentor;
        return Task.FromResult<IActionResult>(NoContent());
    }

    /// <summary>
    /// Deletes the mentor with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the mentor to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that
    /// is <see langword="NoContent"/> if the mentor was deleted successfully; otherwise, <see langword="NotFound"/> if
    /// no mentor with the specified identifier exists.</returns>
    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteMentor(long id)
    {
        var mentor = _mentors.FirstOrDefault(s => s.Id == id);
        if (mentor == null)
            return Task.FromResult<IActionResult>(NotFound());
        _mentors.Remove(mentor);
        return Task.FromResult<IActionResult>(NoContent());
    }
}
