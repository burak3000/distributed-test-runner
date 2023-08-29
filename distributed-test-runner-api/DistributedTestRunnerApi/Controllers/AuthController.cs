using DistributedTestRunner.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DistributedTestRunnerApi.csproj.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(ILogger<AuthController> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("sign-up")]
    public async Task<IActionResult> RegisterUserAsync(PostSignUpRequestDto postSignUpRequestDto)
    {
        var user = new IdentityUser
        {
            Email = postSignUpRequestDto.Email,
            UserName = postSignUpRequestDto.Email,
        };

        var identityResult = await _userManager.CreateAsync(user, postSignUpRequestDto.Password);
        if (identityResult.Succeeded)
        {
            return Ok("New user is created");
        }

        return BadRequest(identityResult);
    }
}
