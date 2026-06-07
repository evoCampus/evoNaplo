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
    public required IEnumerable<string> TeamIds { get; set; } = new List<string>();
    public required IEnumerable<string> ProjectIds { get; set; } = new List<string>();
    public required int SemesterNumber { get; set; }
    public required bool IsActive { get; set; }

    [SetsRequiredMembers]
    public MentorDTO() {
    }

    [SetsRequiredMembers]
    public MentorDTO(Mentor mentor)
    {
        Id = mentor.Id; 
        Name = mentor.Name;
        Email = mentor.Email;
        PhoneNumber = mentor.PhoneNumber;
        TeamIds = mentor.Teams?.Select(t => t.Id).ToList() ?? new List<string>();
        ProjectIds = mentor.Projects?.Select(p => p.Id).ToList() ?? new List<string>();
    }

}
