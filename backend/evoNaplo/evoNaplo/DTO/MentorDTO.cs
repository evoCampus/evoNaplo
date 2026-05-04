namespace evoNaplo.DTO;

public class MentorDTO
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public string? MentorProfile { get; set; }
    public required IEnumerable<string> TeamIds { get; set; }
    public required IEnumerable<string> ProjectIds { get; set; }
    public required int SemesterNumber { get; set; }
    public required bool IsActive { get; set; }

}
