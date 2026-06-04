namespace evoNaplo.Models;

public enum UserRole
{
    Admin,
    Mentor,

}

public class User
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; }
    public string? MentorId { get; set; }
    public Mentor? Mentor { get; set; }
    
}
