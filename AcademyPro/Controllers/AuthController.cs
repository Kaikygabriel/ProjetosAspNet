using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AcademyPro.Models;
using AcademyPro.Models.DTO;
using JwtCraft;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace AcademyPro.Controllers;

[EnableRateLimiting("FixedRate")]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    public AuthController(IConfiguration configuration, UserManager<User> userManager, ITokenService tokenService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    
    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] LoginUserRequest loginUser)
    {
        var userExist = await _userManager.FindByNameAsync(loginUser.Name);
        if (userExist is not null)
            return NotFound("User already exists");
        User user = new()
        {
            SecurityStamp = Guid.NewGuid().ToString("N"),
            UserName = loginUser.Name
        };
        var resultCreate = await _userManager.CreateAsync(user, loginUser.Password);
        if (!resultCreate.Succeeded)
            return BadRequest("Error in create a user");
        return NoContent();
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginUserRequest loginUser)
    {
        var user = await _userManager.FindByNameAsync(loginUser.Name);
        if(user is null || await _userManager.CheckPasswordAsync(user,loginUser.Password))
        {
            List<Claim> claimsUser = new()
            {
                new Claim(ClaimTypes.Name, loginUser.Name),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = _tokenService.GerenateAcessToken(claimsUser, _configuration);
            var refreshToken = _tokenService.GerenateAcessRefreshToken();
            user.RefreshToken = refreshToken;
            user.ExpiredRefreshToken =  DateTime.Now.AddMinutes(JwtOptions.RefreshTokenValidInMinutes);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return NotFound("Error in update user");
            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiredRefreshToken = DateTime.Now.AddMinutes(JwtOptions.RefreshTokenValidInMinutes)
            });
        }

        return Unauthorized();
    }
} 