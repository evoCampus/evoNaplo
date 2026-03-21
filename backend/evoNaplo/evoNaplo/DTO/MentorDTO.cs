namespace evoNaplo.DTO;

public class MentorDTO
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MentorProfile { get; set; }  /// I am not sure if string (for a path/link) is the best type for this, but it can be changed if needed
    public List<string>? Teams { get; set; }
    public List<string>? Projects { get; set; }

}
