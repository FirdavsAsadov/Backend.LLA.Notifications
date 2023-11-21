using Microsoft.AspNetCore.Mvc;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateAccount([FromBody] User user)
    {
        var result = await _accountService.CreateUserAsync(user);
        
        return Ok(result);
    }
}