namespace evoNaplo.Models;

public class User
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required UserRole Role { get; set; }
    public string? MentorId { get; set; }
    public Mentor? Mentor { get; set; }
    
}
