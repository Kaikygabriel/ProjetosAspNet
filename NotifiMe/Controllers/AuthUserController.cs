using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using NotifiMe.Models;
using NotifiMe.Models.LoginModel;
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

    [HttpPost("Refresh")]
    public async Task<ActionResult> RefreshToken([FromBody]LoginTokenModel model)
    {
        var acessToken = model.Token ?? throw new Exception();
        var acessRefreshToken = model.RefreshToken ?? throw new Exception();

        var principal = tokenService.GetPrincipalClaimsExpiredToken(acessToken,configuration);
        var user = await _uwf.UserRepository.GetByIdAsync(x => x.Name == principal.Identity!.Name);
        if (user is null || user.RefreshToken != acessRefreshToken || user.ExpiredRefreshToken <= DateTime.Now)
            return NotFound();
        var newToken = tokenService.GerenateToken(principal.Claims.ToList(),configuration);
        var newRefreshToken = tokenService.GerenateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(10);
        
        _uwf.UserRepository.Update(user);
        await _uwf.CommitAsync();

        return Ok(new
        {
            Token = newToken,
            RefreshToken = newRefreshToken
        });
    }

    [Authorize(Policy = "UserOnly")]
    [HttpPost("Revoke/{UserName:alpha}")]
    public async Task<ActionResult> RevokeRefreshToken(string UserName)
    {
        var user = await _uwf.UserRepository.GetByIdAsync(x => x.Name == UserName);
        if (user is null)
            return NotFound();
        user.RefreshToken = null;
        
        _uwf.UserRepository.Update(user);
        await _uwf.CommitAsync();

        return NoContent();
    }
}