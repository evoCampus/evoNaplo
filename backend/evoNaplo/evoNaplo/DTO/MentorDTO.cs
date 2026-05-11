using System.Diagnostics.CodeAnalysis;
using evoNaplo.Models;

namespace evoNaplo.DTO;

public class MentorDTO
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MentorProfile { get; set; }
    public required IEnumerable<string> TeamIds { get; set; }
    public required IEnumerable<string> ProjectIds { get; set; }
    public required int SemesterNumber { get; set; }
    public required bool IsActive { get; set; }

    [SetsRequiredMembers]
    public MentorDTO(Mentor mentor)
    {
        Id = mentor.Id; 
        Name = mentor.Name;
        Email = mentor.Email;
        PhoneNumber = mentor.PhoneNumber;
        //MentorProfile = mentor.MentorProfile;
        TeamIds = mentor.Teams.Select(team => team.Id).ToList();
        ProjectIds = mentor.Projects.Select(project => project.Id).ToList();
        //SemesterNumber = mentor.SemesterNumber;
        //IsActive = mentor.IsActive;
    }
}
