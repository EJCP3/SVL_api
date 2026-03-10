using Microsoft.AspNetCore.Mvc;
using SVL.Core.Application.DTOs.Auth;
using SVL.Core.Application.Interfaces;

namespace SVL.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        if (!result.TokenValido) return Unauthorized(result.Mensaje);
        return Ok(result);
    }
}