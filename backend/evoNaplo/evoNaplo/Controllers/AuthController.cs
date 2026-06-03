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
            return Ok(await _authService.RegisterAsync(registerData));
        }
        catch (UserWithEmailAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDTO loginData)
    {
        try
        {
            return Ok(await _authService.LoginAsync(loginData));
        }
        catch (Exception ex) when (ex is UserWithGivenEmailNotFoundException or InvalidPasswordException)
        {
            return Unauthorized("Invalid credentials");
        }
    }

}
