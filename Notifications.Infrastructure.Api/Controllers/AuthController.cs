using Microsoft.AspNetCore.Mvc;
using Notifications.Infrastructure.Application.Common.Identity.Models;
using Notifications.Infrastructure.Application.Common.Identity.Services;

namespace Notifications.Infrastructure.Api.Controllers;

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
    public async ValueTask<IActionResult> Register([FromBody] RegistrationDetails registrationDetails)
    {
        var result = await _authService.RegisterAsync(registrationDetails);

        return Ok(result);
    }

    [HttpPost("Login")]
    public async ValueTask<IActionResult> Login([FromBody] LoginDetails loginDetails)
    {
        var result = await _authService.LoginAsync(loginDetails);
        
        return Ok(result);
    }
}