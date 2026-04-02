using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;

[ApiController]
[Route("api/[controller]")]
public class MentorsController : ControllerBase
{
    private static List<MentorDTO> _mentors = new List<MentorDTO>();

    /// <summary>
    /// Retrieves a collection of mentors.
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
    /// <param name="id">The unique identifier of the mentor to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the mentor data as a MentorDTO.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a mentor with the specified identifier does not exist.</exception>
    [HttpGet("{id}")]
    public Task<MentorDTO> GetMentor(string id)
    {
        var mentor = _mentors.FirstOrDefault(s => s.Id == id);
        if (mentor is null)
            throw new KeyNotFoundException($"Mentor with id {id} not found.");
        return Task.FromResult<MentorDTO>(mentor);
    }

    /// <summary>
    /// Creates a new mentor and adds it to the collection.
    /// </summary>
    /// <param name="mentor">A data transfer object containing the details of the mentor to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created mentor data.</returns>
    [HttpPost]
    public Task<MentorDTO> CreateMentor(MentorDTO mentor)
    {
        _mentors.Add(mentor);
        return Task.FromResult<MentorDTO>(mentor);
    }

    /// <summary>
    /// Updates the details of an existing mentor with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the mentor to update. Cannot be null or empty.</param>
    /// <param name="updatedMentor">An object containing the updated mentor information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an IActionResult that is NoContent
    /// if the update is successful, or NotFound if no mentor with the specified identifier exists.</returns>
    [HttpPut("{id}")]
    public Task UpdateMentor(string id, MentorDTO updatedMentor)
    {
        var index = _mentors.FindIndex(s => s.Id == id);
        if (index == -1)
            return Task.FromResult(NotFound());
        _mentors[index] = updatedMentor;
        return Task.FromResult(NoContent());
    }

    /// <summary>
    /// Deletes the mentor with the specified identifier from the collection.
    /// </summary>
    /// <param name="id">The unique identifier of the mentor to delete. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a NoContent result if the mentor is
    /// successfully deleted.</returns>
    [HttpDelete("{id}")]
    public Task DeleteMentor(string id)
    {
        var mentor = _mentors.FirstOrDefault(s => s.Id == id);
        _mentors.Remove(mentor);
        return Task.FromResult(NoContent());
    }
}
