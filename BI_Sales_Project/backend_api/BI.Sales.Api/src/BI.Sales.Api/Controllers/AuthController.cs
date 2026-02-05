using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok(new { token = "sample-token" });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register()
    {
        return Ok();
    }

    [HttpPost("seed")]
    public IActionResult Seed()
    {
        return Ok();
    }
}
