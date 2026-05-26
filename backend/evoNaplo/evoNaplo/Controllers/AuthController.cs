using Microsoft.AspNetCore.Mvc;
using evoNaplo.DTO;
using evoNaplo.Services;
using evoNaplo.Models;

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
            RegisterDTO user = _authService.Register(registerData);
            return Ok("User registered.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDTO loginData)
    {
        LoginDTO? user = _authService.Login(loginData);
        if (user is not null)
        {
            return Ok("Welcome");
        }
        return Unauthorized("Invalid email or password.");
    }

}
