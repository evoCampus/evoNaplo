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
        var result = await _authService.RegisterAsync(registerData);

        if (result.AuthCode != AuthCode.Success)
        {
            if (result.AuthCode == AuthCode.EmailAlreadyInUse)
            {
                return Conflict(result.Message);
            }

            return NotFound(result.Message);
        }

        return Ok(result.User);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDTO loginData)
    {
        var result = await _authService.LoginAsync(loginData);
        
        if (result.AuthCode != AuthCode.Success)
        {
            return Unauthorized(result.Message);
        }

        return Ok(result.User);
    }

}
