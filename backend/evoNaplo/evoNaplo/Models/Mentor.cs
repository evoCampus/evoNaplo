namespace evoNaplo.Models;

public class Mentor
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    // public string? MentorProfile { get; set; }
    // public required bool IsActive { get; set; }
    public required ICollection<Team>? Teams { get; set; }
    public required ICollection<Project>? Projects { get; set; }
}