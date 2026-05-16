namespace evoNaplo.Controllers;

/// <summary>
/// Controller for managing mentors in the application. Provides endpoints for retrieving, creating, updating, and deleting mentor records. Each endpoint interacts with the IMentorService to perform the necessary operations on the mentor data. The controller handles exceptions such as MentorNotFoundException and MentorAlreadyExistsException to return appropriate HTTP responses based on the outcome of each operation.
/// </summary>
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
    /// Retrieves a list of all mentors. If mentors are found, they are returned as a list of MentorDTO objects. If no mentors are found, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <returns>A list of MentorDTO objects if mentors are found; otherwise, a NotFound response.</returns>
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
    /// Retrieves a specific mentor by their unique identifier. If a mentor with the given identifier exists, it is returned as a MentorDTO object. If no mentor is found with the provided identifier, a NotFound response is returned with an appropriate message.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to retrieve.</param>
    /// <returns>The MentorDTO if found; otherwise, a NotFound response.</returns>
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
    /// Creates a new mentor in the database based on the provided MentorDTO object. If a mentor with the same unique identifier already exists, a Conflict response is returned with an appropriate message. If the mentor is created successfully, the newly created MentorDTO object is returned in the response.
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
    /// Updates an existing mentor in the database based on the provided identifier and updated MentorDTO object. If no mentor with the given identifier exists, a NotFound response is returned with an appropriate message. If the mentor is updated successfully, the updated MentorDTO object is returned in the response.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to update. Cannot be null or empty.</param>
    /// <param name="updatedMentor">An object containing the updated mentor information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated mentor.</returns>
    [HttpPut("{mentorId}")]
    public async Task<ActionResult> UpdateMentor(string mentorId, MentorDTO updatedMentor)
    {
        try
        {
            return Ok(await _mentorService.UpdateMentorAsync(mentorId, updatedMentor));
        }
        catch (MentorNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a mentor from the database based on the provided identifier. If no mentor with the given identifier exists, a NotFound response is returned with an appropriate message. If the mentor is deleted successfully, an Ok response is returned indicating the successful deletion.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 200 response if the deletion is successful; otherwise, an HTTP 404 response if the mentor is not found.</returns>
    [HttpDelete("{mentorId}")]
    public async Task<ActionResult> DeleteMentor(string mentorId)
    {
        try
        {
            return Ok(await _mentorService.DeleteMentorAsync(mentorId));
        }
        catch (MentorNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
}
