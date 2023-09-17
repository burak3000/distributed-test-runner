using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DistributedTestRunner.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DistributedTestRunnerApi.csproj.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{



    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(ILogger<AuthController> logger,
        IConfiguration configuration,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn(PostSignUpRequestDto postSignUpRequestDto)
    {
        var loginResult = await _signInManager
                                     .PasswordSignInAsync(postSignUpRequestDto.Email,
                                                          postSignUpRequestDto.Password,
                                                          isPersistent: false,
                                                          lockoutOnFailure: false);

        var user = await _userManager.FindByEmailAsync(postSignUpRequestDto.Email);

        if (!loginResult.Succeeded)
        {

            if (user is null)
            {
                return BadRequest("User not found!");
            }

            return BadRequest("Wrong email and/or password");
        }



        string accessToken = GetToken(user, 10_000);

        string refreshToken = GetToken(user, 12 * 60);

        var tokenResponse = new
        {
            accessToken = accessToken,
            refreshToken = refreshToken,
            roles = new List<string>(),
        };
        return Ok(tokenResponse);
    }

    private string GetToken(IdentityUser user, int expirationMinutes)
    {
        var utcNow = DateTime.UtcNow;

        var claims = new Claim[]
        {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Token:Key")));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            claims: claims,
        notBefore: utcNow,
            expires: utcNow.AddMinutes(expirationMinutes),
            audience: _configuration.GetValue<string>("Token:Audience"),
            issuer: _configuration.GetValue<string>("Token:Issuer")
            );
        string token = new JwtSecurityTokenHandler().WriteToken(jwt);


        return token;

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
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return Ok("New user is created");
        }

        return BadRequest(identityResult);
    }
}
