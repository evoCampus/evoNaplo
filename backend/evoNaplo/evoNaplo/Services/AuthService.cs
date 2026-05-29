using Microsoft.AspNetCore.Identity;
using evoNaplo.Models;
using evoNaplo.DTO;
using evoNaplo.Exceptions;
using evoNaplo.Data;
using Microsoft.EntityFrameworkCore;

namespace evoNaplo.Services;

internal class AuthService : IAuthService
{
    private readonly PasswordHasher<User> _passwordHasher = new();
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDTO> RegisterAsync(RegisterDTO registerData)
    {
        if (await _context.Users.AnyAsync(user => user.Email == registerData.Email))
        {
            throw new UserWithEmailAlreadyExistsException("Email already in use.");
        }
        User user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = registerData.Name,
            Email = registerData.Email,
            Role = UserRole.Mentor,
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, registerData.Password);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return new UserDTO(user);
    }

    public async Task<UserDTO> LoginAsync(LoginDTO loginData)
    {
        User user = await _context.Users.FirstOrDefaultAsync(user => user.Email == loginData.Email) 
            ?? throw new UserWithGivenEmailNotFoundException($"User with email '{loginData.Email}' not found.");
        if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginData.Password) == PasswordVerificationResult.Success)
        {
            return new UserDTO(user);
        }
        throw new InvalidPasswordException($"Invalid password for '{loginData.Email}'.");
    }

}
