using evoNaplo.Models;

namespace evoNaplo.DTO;

public class UserDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public string? MentorId { get; set; }

    public UserDTO(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        Role = user.Role;
        MentorId = user.MentorId;
    }

}
