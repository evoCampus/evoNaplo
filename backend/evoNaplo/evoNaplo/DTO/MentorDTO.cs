namespace evoNaplo.DTO;

public class MentorDTO
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MentorProfile { get; set; }
    public IEnumerable<string>? TeamIds { get; set; }
    public IEnumerable<string>? ProjectIds { get; set; }
    public int SemesterNumber { get; set; }
    public bool IsActive { get; set; }

}
