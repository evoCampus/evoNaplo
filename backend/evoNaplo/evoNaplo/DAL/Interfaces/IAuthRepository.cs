using evoNaplo.Models;

namespace evoNaplo.DAL.Interfaces;

public interface IAuthRepository
{
    public Task<User> GetUserByEmailAsync(string email);
    public Task<bool> AnyUsersAsync();
    public Task AddUserAsync(User user);
    public Task<bool> UserExistsAsync(string email);
    public Task<Mentor> GetMentorByEmailAsync(string email);

}
