using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;
using evoNaplo.Models;

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
    /// Retrieves a list of all mentors in the system, returning their details as MentorDTO objects.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="MentorDTO"/> objects.</returns>
    [HttpGet]
    public Task<IEnumerable<MentorDTO>> GetMentors()
    {
        return Task.FromResult(_mentorService.GetAllMentors().Select(mentor => new MentorDTO 
        { 
            Id = mentor.Id, 
            Name = mentor.Name ?? "N/A", 
            Email = mentor.Email ?? "N/A",
            PhoneNumber = mentor.PhoneNumber ?? "N/A",
            MentorProfile = mentor.MentorProfile ?? "N/A",
            Teams = mentor.Teams?.Select(team => team.Name).ToList() ?? Enumerable.Empty<string>(),
            Projects = mentor.Projects?.Select(project => project.Name).ToList() ?? Enumerable.Empty<string>(),
            SemesterNumber = mentor.SemesterNumber,
            IsActive = mentor.IsActive
        }));
    }

    /// <summary>
    /// Retrieves the details of a specific mentor based on the provided identifier. If no mentor with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="MentorDTO"/> representing
    /// the requested mentor.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a mentor with the specified <paramref name="mentorId"/> does not exist.</exception>
    [HttpGet("{id}")]
    public async Task<ActionResult<MentorDTO>> GetMentor(string mentorId)
    {
        Mentor? mentor = _mentorService.GetMentorById(mentorId);
        if (mentor is null)
            return NotFound($"Mentor with id {mentorId} not found.");
        return Ok(new MentorDTO 
        {
            Id = mentor.Id, 
            Name = mentor.Name ?? "N/A",
            Email = mentor.Email ?? "N/A",
            PhoneNumber = mentor.PhoneNumber ?? "N/A",
            MentorProfile = mentor.MentorProfile ?? "N/A",
            Teams = mentor.Teams?.Select(team => team.Name).ToList() ?? Enumerable.Empty<string>(),
            Projects = mentor.Projects?.Select(project => project.Name).ToList() ?? Enumerable.Empty<string>(),
            SemesterNumber = mentor.SemesterNumber,
            IsActive = mentor.IsActive
        });
    }

    /// <summary>
    /// Creates a new mentor in the system using the provided mentor data. If a mentor with the same identifier already exists, a Conflict response is returned.
    /// </summary>
    /// <param name="mentorToCreate">The mentor data to create. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created mentor.</returns>
    [HttpPost]
    public async Task<ActionResult<Mentor>> CreateMentor(MentorDTO mentorToCreate)
    {
        if (_mentorService.GetMentorById(mentorToCreate.Id) is not null)
            return Conflict($"Mentor with ID {mentorToCreate.Id} already exists.");
        Mentor newMentor = new Mentor
        {
            Id = mentorToCreate.Id,
            Name = mentorToCreate.Name ?? "N/A",
            Email = mentorToCreate.Email ?? "N/A",
            PhoneNumber = mentorToCreate.PhoneNumber ?? "N/A",
            MentorProfile = mentorToCreate.MentorProfile ?? "N/A",
            Teams = mentorToCreate.Teams ?? new List<string>(),
            Projects = mentorToCreate.Projects ?? new List<string>(),
            SemesterNumber = mentorToCreate.SemesterNumber,
            IsActive = mentorToCreate.IsActive
        };
        _mentorService.AddMentor(newMentor);
        return Ok(newMentor);
    }

    /// <summary>
    /// Updates the details of an existing mentor in the system based on the provided identifier and updated mentor data. If no mentor with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to update. Cannot be null or empty.</param>
    /// <param name="updatedMentor">An object containing the updated mentor information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the mentor is not found.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMentor(string mentorId, MentorDTO updatedMentor)
    {
        if (_mentorService.GetMentorById(mentorId) is null)
            return NotFound($"Mentor with ID {mentorId} not found.");
        Mentor mentor = new Mentor
        {
            Id = updatedMentor.Id,
            Name = updatedMentor.Name ?? "N/A",
            Email = updatedMentor.Email ?? "N/A",
            PhoneNumber = updatedMentor.PhoneNumber ?? "N/A",
            MentorProfile = updatedMentor.MentorProfile ?? "N/A",
            Teams = updatedMentor.Teams ?? new List<string>(),
            Projects = updatedMentor.Projects ?? new List<string>(),
            SemesterNumber = updatedMentor.SemesterNumber,
            IsActive = updatedMentor.IsActive
        };
        _mentorService.UpdateMentor(mentorId, mentor);
        return NoContent();
    }

    /// <summary>
    /// Deletes an existing mentor from the system based on the provided identifier. If no mentor with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the deletion is successful; otherwise, an HTTP 404 response if the mentor is not found.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMentor(string mentorId)
    {
        if (_mentorService.GetMentorById(mentorId) is null)
            return NotFound($"Mentor with ID {mentorId} not found.");
        _mentorService.DeleteMentor(mentorId);
        return NoContent();
    }
}
