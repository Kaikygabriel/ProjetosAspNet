using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using NotifiMe.Models;
using NotifiMe.Repository.Interface;
using NotifiMe.Service.Interface;

namespace NotifiMe.Controllers;

[ApiController]
[Route("auth/user")]
public class AuthUserController : ControllerBase
{
    private readonly ITokenService tokenService;
    private readonly IUnitOfWork _uwf;
    private readonly IConfiguration configuration;

    public AuthUserController(ITokenService tokenService, IUnitOfWork uwf, IConfiguration configuration)
    {
        this.tokenService = tokenService;
        _uwf = uwf;
        this.configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] LoginUserModel model )
    {
        var userExist = await _uwf.UserRepository.GetByIdAsync(x => x.Name == model.Name);
        if (userExist is not null)
            return NotFound();
        User user = new User()
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = model.Password
        };
        _uwf.UserRepository.Create(user);
        await _uwf.CommitAsync();
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserModel model)
    {
        var user = await _uwf.UserRepository.GetByIdAsync(x => x.Name == model.Name);
        if (user is null)
            return Unauthorized();
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, "User")
        };

        var token = tokenService.GerenateToken(claims, configuration);
        var refreshToken = tokenService.GerenateRefreshToken();

        user.RefreshToken = refreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(10);
        
        _uwf.UserRepository.Update(user);
        await _uwf.CommitAsync();

        return Ok(new
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpiredRefreshToken = DateTime.Now.AddHours(10)
        });
    }
}