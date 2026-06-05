using Microsoft.AspNetCore.Identity;
using evoNaplo.Models;
using evoNaplo.DTO;
using evoNaplo.Exceptions;
using evoNaplo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace evoNaplo.Services;

internal class AuthService : IAuthService
{
    private readonly PasswordHasher<User> _passwordHasher = new();
    private readonly AppDbContext _context;
    private readonly ILogger<AuthService> _logger;
    private readonly IAuditService _auditService;

    public AuthService(AppDbContext context, ILogger<AuthService> logger, IAuditService auditService)
    {
        _context = context;
        _logger = logger;
        _auditService = auditService;
    }

    public async Task<UserDTO> RegisterAsync(RegisterDTO registerData)
    {
        if (!await _context.Users.AnyAsync())
        {
            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerData.Email,
                Role = UserRole.Admin,
                MentorId = null,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, registerData.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User registered {UserId} {Email}", user.Id, user.Email);
            await _auditService.LogAsync(new AuditLog
            {
                EventType = "Register",
                Resource = "User",
                Action = "Register",
                Outcome = "Success",
                UserId = user.Id,
                Details = $"First user created: {user.Email}"
            });
            return new UserDTO(user);
        }
        else if (await _context.Users.AnyAsync(user => user.Email == registerData.Email))
        {
            _logger.LogWarning("Register attempt with already used email {Email}", registerData.Email);
            await _auditService.LogAsync(new AuditLog
            {
                EventType = "RegisterAttempt",
                Resource = "User",
                Action = "Register",
                Outcome = "Failure",
                Details = $"Email {registerData.Email} already in use"
            });
            throw new UserWithEmailAlreadyExistsException("Email already in use.");
        }
        var mentor = await _context.Mentors.FirstOrDefaultAsync(m => m.Email == registerData.Email);
        if (mentor != null)
        {
            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerData.Email,
                Role = UserRole.Mentor,
                MentorId = mentor.Id,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, registerData.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User registered {UserId} {Email}", user.Id, user.Email);
            await _auditService.LogAsync(new AuditLog
            {
                EventType = "Register",
                Resource = "User",
                Action = "Register",
                Outcome = "Success",
                UserId = user.Id,
                Details = $"User {user.Email} created"
            });
            return new UserDTO(user);
        }
        else
        {
            _logger.LogWarning("Register attempt with non-mentor email {Email}", registerData.Email);
            await _auditService.LogAsync(new AuditLog
            {
                EventType = "RegisterAttempt",
                Resource = "User",
                Action = "Register",
                Outcome = "Failure",
                Details = $"Mentor with email {registerData.Email} not found"
            });
            throw new MentorWithGivenEmailNotFoundException("Mentor with given email not found.");
        }
    }

    public async Task<UserDTO> LoginAsync(LoginDTO loginData)
    {
        User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginData.Email) 
            ?? throw new UserWithGivenEmailNotFoundException($"User with email '{loginData.Email}' not found.");
        if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginData.Password) == PasswordVerificationResult.Success)
        {
            _logger.LogInformation("User login success {UserId} {Email}", user.Id, user.Email);
            await _auditService.LogAsync(new AuditLog
            {
                EventType = "Login",
                Resource = "User",
                Action = "Login",
                Outcome = "Success",
                UserId = user.Id,
                Details = $"User {user.Email} logged in"
            });
            return new UserDTO(user);
        }
        _logger.LogWarning("User login failed (invalid password) {Email}", loginData.Email);
        await _auditService.LogAsync(new AuditLog
        {
            EventType = "Login",
            Resource = "User",
            Action = "Login",
            Outcome = "Failure",
            Details = $"Invalid password for {loginData.Email}"
        });
        throw new InvalidPasswordException($"Invalid password for '{loginData.Email}'.");
    }

}
