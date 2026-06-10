using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.DAL.Interfaces;

public interface IAuthRepository
{
    public Task<bool> AnyUsersAsync();
    public Task AddUserAsync(User user);
    public Task<bool> UserExistsAsync(string email);
    public Task<Mentor?> GetMentorByEmailAsync(string email);
    public Task<User?> GetUserByEmailAsync(string email);

}
