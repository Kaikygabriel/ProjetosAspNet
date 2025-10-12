using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EduCore.Application.DTOS.Provider;
using EduCore.Application.Interfaces;
using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProvidersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    
    public ProvidersController(IUnitOfWork unitOfWork,ITokenService tokenService,IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    private IEnumerable<Claim> GetClaimsFromUser(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        foreach (var role in user.GetRoles()!)
            claims.Add(new Claim(ClaimTypes.Role,role)); 
        return claims;
    }
    
    
    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterProviderDto model)
    {
        if (model is null)
            return BadRequest();
        User? userExist = await _unitOfWork.RepositoryUser.GetByPredicateAsync(x =>x.Name==model.Name);
        if(userExist is not null)
            return NotFound();

        var user = new User(model.Name, model.Password);
        var provider = new Provider(user, new Email(model.AdressEmail));
        user.SetRoles("Provider");

        
        _unitOfWork.RepositoryUser.Create(user);
        _unitOfWork.RepositoryProvider.Create(provider);

        await _unitOfWork.CommitAsync();
        return Created();
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginProviderDto model)
    {
        if (model is null)
            return BadRequest();
        User? user = await _unitOfWork.RepositoryUser.GetByPredicateAsync(x =>x.Name==model.Name);
        if (user is null || !user.CheckPassword(model.Password) || 
            !user.GetRoles()!.Exists(x => x == "Provider")) 
            return NotFound();
        
        var claims = GetClaimsFromUser(user);

        var token = _tokenService.GerenateAcessToken(claims, _configuration);
        var refreshToken = _tokenService.GerenateRefreshToken();

        user.RefreshToken = refreshToken;
        user.ExpiredRefreshToken = DateTime.UtcNow.AddDays(4);
        
        _unitOfWork.RepositoryUser.Update(user);
        await _unitOfWork.CommitAsync();

        return Ok(new
        {
            token = token,
            refreshToken = refreshToken,
            expiredRefreshToken = DateTime.UtcNow.AddDays(4)
        });
    }
}