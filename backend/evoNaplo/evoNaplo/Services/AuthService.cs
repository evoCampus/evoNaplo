using Microsoft.AspNetCore.Identity;
using evoNaplo.Models;
using evoNaplo.DTO;
using evoNaplo.Exceptions;
using evoNaplo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using evoNaplo.DAL.Interfaces;

namespace evoNaplo.Services;

internal class AuthService : IAuthService
{
    private readonly PasswordHasher<User> _passwordHasher = new();
    private readonly IAuthRepository _authRepository;
    private readonly ILogger<AuthService> _logger;
    private readonly IAuditService _auditService;

    public AuthService(IAuthRepository authRepository, ILogger<AuthService> logger, IAuditService auditService)
    {
        _authRepository = authRepository;
        _logger = logger;
        _auditService = auditService;
    }

    public async Task<UserDTO> RegisterAsync(RegisterDTO registerData)
    {
        if ((await _authRepository.AnyUsersAsync()) == false)
        {
            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerData.Email,
                Role = UserRole.Admin,
                MentorId = null,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, registerData.Password);
            await _authRepository.AddUserAsync(user);
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
        else if (await _authRepository.UserExistsAsync(registerData.Email))
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
        try
        {
            var mentor = await _authRepository.GetMentorByEmailAsync(registerData.Email);
            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerData.Email,
                Role = UserRole.Mentor,
                MentorId = mentor.Id,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, registerData.Password);
            await _authRepository.AddUserAsync(user);
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
        catch (MentorWithGivenEmailNotFoundException)
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
            throw;
        }
    }

    public async Task<UserDTO> LoginAsync(LoginDTO loginData)
    {
        User user = await _authRepository.GetUserByEmailAsync(loginData.Email);
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
