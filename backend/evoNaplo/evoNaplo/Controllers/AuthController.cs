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

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDTO registerData)
    {
        try {
            return Ok(await _authService.RegisterAsync(registerData));
        }
        catch (MentorWithGivenEmailNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UserWithEmailAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (PasswordTooShortException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (PasswordMismatchException ex)
        {
            return BadRequest(ex.Message);
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
