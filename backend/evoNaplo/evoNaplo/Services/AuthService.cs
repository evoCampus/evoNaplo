using Microsoft.AspNetCore.Identity;
using evoNaplo.Models;
using evoNaplo.DTO;
using evoNaplo.Exceptions;

namespace evoNaplo.Services;

internal class AuthService : IAuthService
{
    private static readonly List<User> _users = [];
    private readonly PasswordHasher<User> _passwordHasher = new();

    public async Task<UserDTO> RegisterAsync(RegisterDTO registerData)
    {
        if (_users.Any(user => user.Email == registerData.Email))
        {
            throw new UserWithEmailAlreadyExistsException("Email already in use.");
        }
        User user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = registerData.Name,
            Email = registerData.Email,
            Role = UserRole.Mentor
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, registerData.Password);
        _users.Add(user);
        return new UserDTO(user);
    }

    public async Task<UserDTO> LoginAsync(LoginDTO loginData)
    {
        User user = _users.FirstOrDefault(user => user.Email == loginData.Email) 
            ?? throw new UserWithGivenEmailNotFoundException($"User with email '{loginData.Email}' not found.");
        if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginData.Password) == PasswordVerificationResult.Success)
        {
            return new UserDTO(user);
        }
        throw new InvalidPasswordException($"Invalid password for '{loginData.Email}'.");
    }

}
