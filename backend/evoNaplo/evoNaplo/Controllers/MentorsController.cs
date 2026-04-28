using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;
using evoNaplo.Models;

[ApiController]
[Route("api/[controller]")]
internal class MentorsController : ControllerBase
{
    private readonly IMentorService _mentorService;
    private readonly ITeamService _teamService;
    private readonly IProjectService _projectService;

    public MentorsController(IMentorService mentorService, ITeamService teamService, IProjectService projectService)
    {
        _mentorService = mentorService;
        _teamService = teamService;
        _projectService = projectService;
    }

    /// <summary>
    /// Retrieves a list of all mentors in the system, returning their details as MentorDTO objects. If no mentors are found, an empty collection is returned.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of mentor data transfer objects.</returns>
    [HttpGet]
    public Task<IEnumerable<MentorDTO>> GetMentors()
    {
        return Task.FromResult(_mentorService.GetAllMentors().Select(mentor => new MentorDTO 
        { 
            Id = mentor.Id, 
            Name = mentor.Name ?? "N/A", 
            Email = mentor.Email ?? "N/A",
            PhoneNumber = mentor.PhoneNumber ?? "N/A",
            TeamIds = mentor.Teams?.Select(team => team.Id).ToList() ?? Enumerable.Empty<string>(),
            ProjectIds = mentor.Projects?.Select(project => project.Id).ToList() ?? Enumerable.Empty<string>(),
        }));
    }

    /// <summary>
    /// Retrieves the details of a specific mentor based on the provided identifier. If no mentor with the given identifier exists, a NotFound response is returned.   
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to retrieve. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see
    [HttpGet("{mentorId}")]
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
            TeamIds = mentor.Teams?.Select(team => team.Id).ToList() ?? Enumerable.Empty<string>(),
            ProjectIds = mentor.Projects?.Select(project => project.Id).ToList() ?? Enumerable.Empty<string>(),
        });
    }

    /// <summary>
    /// Creates a new mentor in the system based on the provided mentor details. If a mentor with the same identifier already exists, a Conflict response is returned. The created mentor is returned in the response body upon successful creation.
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
            Teams = mentorToCreate.TeamIds?.Select(teamId => _teamService.GetTeamById(teamId)).OfType<Team>().ToList() ?? new List<Team>(),
            Projects = mentorToCreate.ProjectIds?.Select(projectId => _projectService.GetProjectById(projectId)).OfType<Project>().ToList() ?? new List<Project>(),
        };
        _mentorService.AddMentor(newMentor);
        return Ok(newMentor);
    }

    /// <summary>
    /// Updates the details of an existing mentor in the system based on the provided identifier and updated mentor data. If no mentor with the given identifier exists, a NotFound response is returned. Upon successful update, an HTTP 204 No Content response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to update. Cannot be null or empty.</param>
    /// <param name="updatedMentor">An object containing the updated mentor information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the update is successful; otherwise, an HTTP 404 response if the mentor is not found.</returns>
    [HttpPut("{mentorId}")]
    public async Task<ActionResult> UpdateMentor(string mentorId, MentorDTO updatedMentor)
    {
        if (_mentorService.GetMentorById(mentorId) is null)
            return NotFound($"Mentor with ID {mentorId} not found.");
        Mentor mentor = new Mentor
        {
            Name = updatedMentor.Name ?? "N/A",
            Email = updatedMentor.Email ?? "N/A",
            PhoneNumber = updatedMentor.PhoneNumber ?? "N/A",
            Teams = updatedMentor.TeamIds?.Select(teamId => _teamService.GetTeamById(teamId)).OfType<Team>().ToList() ?? new List<Team>(),
            Projects = updatedMentor.ProjectIds?.Select(projectId => _projectService.GetProjectById(projectId)).OfType<Project>().ToList() ?? new List<Project>(),
        };
        _mentorService.UpdateMentor(mentorId, mentor);
        return NoContent();
    }

    /// <summary>
    /// Deletes an existing mentor from the system based on the provided identifier. If no mentor with the given identifier exists, a NotFound response is returned.
    /// </summary>
    /// <param name="mentorId">The unique identifier of the mentor to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 204 response if the deletion is successful; otherwise, an HTTP 404 response if the mentor is not found.</returns>
    [HttpDelete("{mentorId}")]
    public async Task<ActionResult> DeleteMentor(string mentorId)
    {
        if (_mentorService.GetMentorById(mentorId) is null)
            return NotFound($"Mentor with ID {mentorId} not found.");
        _mentorService.DeleteMentor(mentorId);
        return NoContent();
    }
}
