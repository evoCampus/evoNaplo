namespace evoNaplo.Models;

public class RegisterDTO
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required UserRole Role { get; set; }
    public string? MentorId { get; set; }

}
