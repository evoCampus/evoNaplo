namespace evoNaplo.Models;

public class User
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; }
    public string? MentorId { get; set; }
    public Mentor? Mentor { get; set; }
    
    
}
