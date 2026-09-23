using Microsoft.AspNetCore.Mvc;
using People_Specification.Api.DTOs;
using People_Specification.Api.Services;

namespace People_Specification.Api.Controllers;

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
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        await _authService.RegisterAsync(request);

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var token = await _authService.LoginAsync(request);

        if (token is null)
        {
            return Unauthorized("Invalid username or password.");
        }

        return Ok(new
        {
            token
        });
    }
}
