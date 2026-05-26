using Microsoft.AspNetCore.Identity;
using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services;

internal class AuthService : IAuthService
{
    private static readonly List<User> _users = new();
    private readonly PasswordHasher<User> _passwordHasher = new();

    public RegisterDTO Register(RegisterDTO registerData)
    {
        if (_users.Any(user => user.Email == registerData.Email))
            throw new Exception("Email already in use.");
        User user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = registerData.Name,
            Email = registerData.Email,
            PasswordHash = string.Empty,
            Role = registerData.Role,
            MentorId = registerData.MentorId
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, registerData.Password);
        _users.Add(user);
        return registerData;
    }

    public LoginDTO? Login(LoginDTO loginData)
    {
        User? user = _users.FirstOrDefault(user => user.Email == loginData.Email);
        if (user is null)
        {
            return null;
        }
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginData.Password);
        if (result == PasswordVerificationResult.Success)
        {
            return loginData;
        }
        return null;
    }

}
