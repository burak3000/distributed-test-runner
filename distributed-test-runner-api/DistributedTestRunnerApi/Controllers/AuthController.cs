using DistributedTestRunner.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DistributedTestRunnerApi.csproj.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("sign-up")]
    public IActionResult RegisterUser(PostSignUpRequestDto postSignUpRequestDto)
    {
        return Ok("User is saved");
    }
}
