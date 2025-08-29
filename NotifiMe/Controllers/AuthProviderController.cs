using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using NotifiMe.Extesion;
using NotifiMe.Models;
using NotifiMe.Models.LoginModel;
using NotifiMe.Repository.Interface;
using NotifiMe.Service.Interface;

namespace NotifiMe.Controllers;

[ApiController]
[Route("auth/provider")]
public class AuthProviderController : ControllerBase
{
    
    private readonly ITokenService tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration configuration;

    public AuthProviderController(ITokenService tokenService, IUnitOfWork uwf, IConfiguration configuration)
    {
        this.tokenService = tokenService;
        _unitOfWork = uwf;
        this.configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterAsync([FromBody]LoginProviderModel model)
    {
        if (model is null)
            return NotFound();
        var userExist = await _unitOfWork.ProviderRepository.GetByIdAsync(x => x.Name == model.Name!);
        if (userExist is not null)
            return NotFound();
        if (model.Password.Length < 6)
            return BadRequest("The password length is small, it must be greater than 6");
        Provider user = new()
        {   
            Name = model.Name,
            PasswordHash = model.Password,
            Email = model.Email,
            Work = model.Work
        };
        
        _unitOfWork.ProviderRepository.Create(user);
        await _unitOfWork.CommitAsync();
        
        return NoContent();
    }
    [HttpPost("Login")] 
    public async Task<ActionResult> LoginAsync([FromBody]LoginProviderModel model)
    {
        var user= await _unitOfWork.ProviderRepository.GetByIdAsync(x => x.Name == model.Name!);
        if (user is null || !user.CheckPassword(model.Password))
            return Unauthorized();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Name),
            new Claim(ClaimTypes.Email, model.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role,"Provider")
        };

        var token = tokenService.GerenateToken(claims, configuration);
        var refreshToken = tokenService.GerenateRefreshToken();

        user.RefreshToken = refreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(10);
        
        _unitOfWork.ProviderRepository.Update(user);
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
        var acessRefreshToken = model.RefreshToken ??throw new Exception();

        var principal = tokenService.GetPrincipalClaimsExpiredToken(acessToken, configuration);
        var user = await _unitOfWork.ProviderRepository.
            GetByIdAsync(x => x.Name == principal.Identity!.Name!);
        if (user is null || user.RefreshToken != acessRefreshToken || user.ExpiredRefreshToken <= DateTime.Now)
            return BadRequest();

        var newToken = tokenService.GerenateToken(principal.Claims.ToList(), configuration);
        var newRefreshToken = tokenService.GerenateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(10);
        
        _unitOfWork.ProviderRepository.Update(user);
        await _unitOfWork.CommitAsync();

        return Ok(new
        {
            token = newToken,
            refreshToken = newRefreshToken
        });
    }

    [Authorize(Policy ="ProviderOnly")]
    [HttpPost("Revoke/{userName:alpha}")]
    public async Task<ActionResult> Revoke(string userName)
    {
        var user =await _unitOfWork.ProviderRepository.GetByIdAsync(x => x.Name == userName);
        if (user is null)
            return NotFound();
        user.RefreshToken = null;
        await _unitOfWork.CommitAsync();
        return NoContent();
    }
}