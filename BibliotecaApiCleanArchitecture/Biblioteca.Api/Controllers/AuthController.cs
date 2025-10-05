using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Biblioteca.Application.DTOS;
using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.BackOffice.Entities;
using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Domain.BackOffice.ObjectValues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOf;

    public AuthController(IUnitOfWork unitOf, ITokenService tokenService, IConfiguration configuration)
    {
        _unitOf = unitOf;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    private IEnumerable<Claim> GetClaimsFromUser(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user), "O usuário não pode ser nulo.");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email?.Adress ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (user.Roles != null)
        {
            foreach (var role in user.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
    
    
    [EnableRateLimiting("Fixed")]
    [HttpPost("Register")]
    public async Task<ActionResult> RegisterUserAsync([FromBody] RegisterUserDTO userModel)
    {
        if (userModel is null)
            return BadRequest();
        var userExist = await _unitOf.RepositoryUser.GetByPredicate(x => x.Name == userModel.Name);
        if (userExist is not null)
            return NotFound();
        var user = new User
        {
            Name = userModel.Name,
            Email = new Email(userModel.EmailAdress),
            Password = userModel.Password
        };
        _unitOf.RepositoryUser.Create(user);
        await _unitOf.CommitAsync();
        
        return Created();
    }

    [EnableRateLimiting("Fixed")]
    [HttpPost("Login")]
    public async Task<ActionResult> LoginUserAsync([FromBody] LoginUserDTO userModel)
    {
        if (userModel is null || userModel.Name is null || userModel.Password is null)
            return BadRequest();
        
        var user = await _unitOf.RepositoryUser.GetByPredicate(x => x!.Name == userModel.Name);
        if (user is null || !user.CheckPassword(userModel.Password))
            return NotFound();
        
        var claims = GetClaimsFromUser(user);
        var token = _tokenService.GerenateAcessToken(claims,_configuration);
        var refreshToken = _tokenService.GerenateRefreshToken();

        user.RefreshToken = refreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(30);

        return Ok(new
        {
            token = token,
            refreshToken = refreshToken,
            ExpiredRefreshToken = DateTime.Now.AddHours(30)
        });
    }
}