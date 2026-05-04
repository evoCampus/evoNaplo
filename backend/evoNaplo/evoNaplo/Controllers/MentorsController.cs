using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;

[ApiController]
[Route("api/[controller]")]
internal class MentorsController : ControllerBase
{
    private readonly IMentorService _mentorService;

    public MentorsController(IMentorService mentorService)
    {
        _mentorService = mentorService;
    }

    /// <summary>
    /// Retrieves a list of all mentors in the system. Each mentor is represented as a <see cref="MentorDTO"/> object containing the mentor's details. If no mentors are found, an empty list is returned.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="MentorDTO"/> objects representing all mentors in the system.</returns>
    [HttpGet]
    public Task<IEnumerable<MentorDTO>> GetMentors()
    {
        return Task.FromResult(_mentorService.GetAllMentors());
    }

    /// <summary>
    /// Retrieves the details of a specific mentor based on the provided identifier. If a mentor with the given identifier exists, their details are returned as a <see cref="MentorDTO"/> object. If no mentor is found with the specified identifier, a NotFound response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="MentorDTO"/> object representing the requested mentor, or a NotFound response if the mentor is not found.</returns>
    [HttpGet("{mentorId}")]
    public async Task<ActionResult<MentorDTO>> GetMentor(string mentorId)
    {
        MentorDTO? mentor = _mentorService.GetMentorById(mentorId);
        if (mentor is not null)
        {
            return Ok(mentor);
        }
        else
        {
            return NotFound($"Mentor with id {mentorId} not found.");
        }
    }

    /// <summary>
    /// Creates a new mentor in the system based on the provided mentor data. The mentor details are provided as a <see cref="MentorDTO"/> object in the request body. If a mentor with the same identifier already exists, a Conflict response is returned. Upon successful creation, the newly created mentor's details are returned in the response.
    /// </summary>
    /// <param name="mentorToCreate">The mentor data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created mentor.</returns>
    [HttpPost]
    public async Task<ActionResult<MentorDTO>> CreateMentor(MentorDTO mentorToCreate)
    {
        if (_mentorService.GetMentorById(mentorToCreate.Id) is null)
        {
            _mentorService.AddMentor(mentorToCreate);
            return Ok(mentorToCreate);
        }
        else
        {
            return Conflict($"Mentor with ID {mentorToCreate.Id} already exists.");
        }
    }

    /// <summary>
    /// Updates the details of an existing mentor based on the provided identifier and updated mentor data. The mentor to update is identified by the <paramref name="mentorId"/> parameter, and the updated mentor details are provided as a <see cref="MentorDTO"/> object in the request body. If no mentor with the given identifier exists, a NotFound response is returned. Upon successful update, an HTTP 204 No Content response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to update. Cannot be null or empty.</param>
    /// <param name="updatedMentor">An object containing the updated mentor information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the mentor is not found.0</returns>
    [HttpPut("{mentorId}")]
    public async Task<ActionResult> UpdateMentor(string mentorId, MentorDTO updatedMentor)
    {
        if (_mentorService.GetMentorById(mentorId) is not null)
        {
            _mentorService.UpdateMentor(mentorId, updatedMentor);
            return NoContent();
        }
        else
        {
            return NotFound($"Mentor with ID {mentorId} not found.");
        }
    }

    /// <summary>
    /// Deletes an existing mentor from the system based on the provided identifier. If no mentor with the given identifier exists, a NotFound response is returned. Upon successful deletion, an HTTP 204 No Content response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the deletion is successful; otherwise, an HTTP 404 response if the mentor is not found.</returns>
    [HttpDelete("{mentorId}")]
    public async Task<ActionResult> DeleteMentor(string mentorId)
    {
        if (_mentorService.GetMentorById(mentorId) is not null)
        {
            _mentorService.DeleteMentor(mentorId);
            return NoContent();
        }
        else
        {
            return NotFound($"Mentor with ID {mentorId} not found.");
        }
    }
}
