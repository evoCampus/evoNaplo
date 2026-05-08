using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;

[ApiController]
[Route("api/[controller]")]
public class MentorsController : ControllerBase
{
    private readonly IMentorService _mentorService;

    public MentorsController(IMentorService mentorService)
    {
        _mentorService = mentorService;
    }

    /// <summary>
    /// Retrieves a list of all mentors in the database. If no mentors are found, a NotFound response is returned with an appropriate message. Each mentor is returned as a MentorDTO.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="MentorDTO"/> objects representing all mentors in the database.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MentorDTO>>> GetMentors()
    {
        try
        {
            return Ok(await _mentorService.GetAllMentorsAsync());
        }
        catch (MentorNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a specific mentor by their ID. If a mentor with the specified ID is found, it is returned as a MentorDTO. If no mentor is found with the given ID, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <param name="mentorId">The ID of the mentor to retrieve.</param>
    /// <returns>The MentorDTO if found, otherwise a NotFound response.</returns>
    [HttpGet("{mentorId}")]
    public async Task<ActionResult<MentorDTO>> GetMentor(string mentorId)
    {
        try
        {
            return Ok(await _mentorService.GetMentorByIdAsync(mentorId));
        }
        catch (MentorNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Adds a new mentor to the database. If a mentor with the same ID already exists, a Conflict response is returned with an appropriate message. If the mentor is added successfully, the created MentorDTO is returned in the response body.
    /// </summary>
    /// <param name="mentorToCreate">The mentor data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created mentor.</returns>
    [HttpPost]
    public async Task<ActionResult<MentorDTO>> CreateMentor(MentorDTO mentorToCreate)
    {
        try
        {
            return Ok(await _mentorService.AddMentorAsync(mentorToCreate));
        }
        catch (MentorAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing mentor in the database based on the provided identifier and the provided updated mentor data. If no mentor with the given identifier exists, a NotFound response is returned with an appropriate message. If the mentor is updated successfully, a NoContent response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to update. Cannot be null or empty.</param>
    /// <param name="updatedMentor">An object containing the updated mentor information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the mentor is not found.</returns>
    [HttpPut("{mentorId}")]
    public async Task<ActionResult> UpdateMentor(string mentorId, MentorDTO updatedMentor)
    {
        try
        {
            await _mentorService.UpdateMentorAsync(mentorId, updatedMentor);
            return NoContent();
        }
        catch (MentorNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a mentor from the database based on the provided identifier. If no mentor with the given identifier exists, a NotFound response is returned with an appropriate message. If the mentor is deleted successfully, a NoContent response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the deletion is successful; otherwise, an HTTP 404 response if the mentor is not found.</returns>
    [HttpDelete("{mentorId}")]
    public async Task<ActionResult> DeleteMentor(string mentorId)
    {
        try
        {
            await _mentorService.DeleteMentorAsync(mentorId);
            return NoContent();
        }
        catch (MentorNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
