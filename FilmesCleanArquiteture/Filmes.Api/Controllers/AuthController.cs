using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Filmes.Application.DTOS;
using Filmes.Application.Interfaces;
using Filmes.Application.Services;
using Filmes.Application.Services.Interfaces;
using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IServiceRepositoryUser _serviceRepositoryUser;
    private readonly ITokenService _tokenService;
    
    public AuthController(IServiceRepositoryUser serviceRepositoryUser, ITokenService tokenService, IConfiguration config)
    {
        _serviceRepositoryUser = serviceRepositoryUser;
        _tokenService = tokenService;
        this.config = config;
    }
    

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUserAsync(RegisterModel model)
    {
        if (model is null)
            return NotFound();
        var user = await _serviceRepositoryUser.GetByName(model.Name!);
        if (user is not null || model.Password is null || model.Password!.Length < 6)
            return BadRequest();
        User userCreate = new()
        {
            Name = model.Name!,
            Email = model.Email,
            PasswordHash = model.Password!
        };
         _serviceRepositoryUser.Create(userCreate);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUserAsync(LoginModel model)
    {
        if (model is null)
            return BadRequest();
        var user = await _serviceRepositoryUser.GetByName(model.Name!);
        if (user is null || !user.CheckPassword(model.Password!))
            return NotFound();
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = _tokenService.GerenateAcessToken(claims, config);
        var refreshToken = _tokenService.GerenateAcessRefreshToken();

        user.RefreshToken = refreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(30);

        await _serviceRepositoryUser.Update(user);

        return Ok(new
        {
            token = token,
            refreshToken = refreshToken
        });
    }
    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken(TokenLoginDTO model)
    {
        if (model is null)
            return BadRequest();

        var principal = _tokenService.GetClaimsPrincipalFromExpiredToken(model.AccessToken!, config);

        var userName = principal.Identity?.Name ?? throw new Exception("Invalid access token");

        var user = await _serviceRepositoryUser.GetByName(userName!);
        if (user is null || user.RefreshToken != model.RefreshToken || user.ExpiredRefreshToken <= DateTime.Now)
            return BadRequest(new { message = "Invalid client request" });

        var newAccessToken = _tokenService.GerenateAcessToken(principal.Claims.ToList(), config);
        var newRefreshToken = _tokenService.GerenateAcessRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(30);
        
        await _serviceRepositoryUser.Update(user);
        return Ok(new
        {
            token = newAccessToken,
            refreshToken = newRefreshToken
        });
    }
}