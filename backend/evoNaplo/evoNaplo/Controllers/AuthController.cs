using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;
using evoNaplo.Exceptions;

namespace evoNaplo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    private readonly IAuditService _auditService;

    public AuthController(IAuthService authService, ILogger<AuthController> logger, IAuditService auditService)
    {
        _authService = authService;
        _logger = logger;
        _auditService = auditService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDTO registerData)
    {
        try {
            var result = await _authService.RegisterAsync(registerData);
            _logger.LogInformation("Register endpoint success for {Email}", registerData.Email);
            await _auditService.LogAsync(new Models.AuditLog
            {
                EventType = "RegisterEndpoint",
                Resource = "User",
                Action = "Register",
                Outcome = "Success",
                Details = $"Registered {registerData.Email}"
            });
            return Ok(result);
        }
        catch (UserWithEmailAlreadyExistsException ex)
        {
            _logger.LogWarning("Register endpoint attempt with existing email {Email}", registerData.Email);
            await _auditService.LogAsync(new Models.AuditLog
            {
                EventType = "RegisterEndpoint",
                Resource = "User",
                Action = "Register",
                Outcome = "Failure",
                Details = ex.Message
            });
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDTO loginData)
    {
        try
        {
            var result = await _authService.LoginAsync(loginData);
            _logger.LogInformation("Login endpoint success for {Email}", loginData.Email);
            await _auditService.LogAsync(new Models.AuditLog
            {
                EventType = "LoginEndpoint",
                Resource = "User",
                Action = "Login",
                Outcome = "Success",
                Details = $"Login {loginData.Email}"
            });
            return Ok(result);
        }
        catch (Exception ex) when (ex is UserWithGivenEmailNotFoundException or InvalidPasswordException)
        {
            _logger.LogWarning("Login endpoint failed for {Email}: {Message}", loginData.Email, ex.Message);
            await _auditService.LogAsync(new Models.AuditLog
            {
                EventType = "LoginEndpoint",
                Resource = "User",
                Action = "Login",
                Outcome = "Failure",
                Details = ex.Message
            });
            return Unauthorized("Invalid credentials");
        }
    }

}
