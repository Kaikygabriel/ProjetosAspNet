using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using NotifiMe.Extesion;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration configuration;

    public AuthUserController(ITokenService tokenService, IUnitOfWork uwf, IConfiguration configuration)
    {
        this.tokenService = tokenService;
        _unitOfWork = uwf;
        this.configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] LoginUserModel model )
    {
        var userExist = await _unitOfWork.UserRepository.GetByPredicateAsync(x => x.Name == model.Name);
        if (userExist is not null)
            return NotFound();
        if(model.Password.Length < 6)
               return BadRequest("The password length is small, it must be greater than 6");
        User user = new User()
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = model.Password
        };
        _unitOfWork.UserRepository.Create(user);
        await _unitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserModel model)
    {
        var user = await _unitOfWork.UserRepository.GetByPredicateAsync(x => x.Name == model.Name);
        if (user is  null|| !user.CheckPassword(model.Password))
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
        
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CommitAsync();

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
        var user = await _unitOfWork.UserRepository.GetByPredicateAsync(x => x.Name == principal.Identity!.Name);
        if (user is null || user.RefreshToken != acessRefreshToken || user.ExpiredRefreshToken <= DateTime.Now)
            return NotFound();
        var newToken = tokenService.GerenateToken(principal.Claims.ToList(),configuration);
        var newRefreshToken = tokenService.GerenateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(10);
        
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CommitAsync();

        return Ok(new
        {
            Token = newToken,
            RefreshToken = newRefreshToken
        });
    }

    [Authorize("UserOnly")]
    [HttpPost("Revoke/{UserName:alpha}")]
    public async Task<ActionResult> RevokeRefreshToken(string UserName)
    {
        var user = await _unitOfWork.UserRepository.GetByPredicateAsync(x => x.Name == UserName);
        if (user is null)
            return NotFound();
        user.RefreshToken = null;
        
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CommitAsync();

        return NoContent();
    }
}