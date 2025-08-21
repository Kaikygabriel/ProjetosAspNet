using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using ApiClientes.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ApiClientes.Model;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ApiClientes.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _userRole;

    public AuthController(ITokenService tokenService, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> userRole)
    {
        _tokenService = tokenService;
        _configuration = configuration;
        _userManager = userManager;
        _userRole = userRole;
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName!);
        if (user is not null && await _userManager.CheckPasswordAsync(user, model.UserPassword!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };
            foreach (var userRole in userRoles)
            {
                claim.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GerenateAcessToken(_configuration, claim);
            var refreshToken = _tokenService.GerenateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpired = DateTime.Now.AddHours(20);
            await _userManager.UpdateAsync(user);
            
            return Ok(new
            {
                RefreshToken = refreshToken,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expired = token.ValidTo
            });
        }
        return Unauthorized();
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] LoginModel model)
    {
        var userExist = await _userManager.FindByNameAsync(model.UserName!);
        if (userExist is not null)
            return BadRequest("User exist");
        ApplicationUser user = new()
        {
            UserName = model.UserName
        };
        var resultCreated = await _userManager.CreateAsync(user,model.UserPassword!);
        if (!resultCreated.Succeeded)
            return NotFound("Falid in Created");
        return Ok();
    }

    [HttpPost("Refresh-Token")]
    public async Task<ActionResult> RefreshTokenRegister([FromBody] TokenModel model)
    {
        var acessToken = model.Token ?? throw new Exception();
        var RefreshToken = model.RefreshToken ?? throw new Exception();

        var principal = _tokenService.ClaimExpiredToken(acessToken, _configuration);
        if (principal is null)
            return BadRequest();
        
        var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);
        var newRefreshtoken = _tokenService.GerenateRefreshToken();
        if (model.RefreshToken != RefreshToken || user.RefreshTokenExpired <= DateTime.Now)
            return BadRequest();
        user.RefreshToken = newRefreshtoken;
        user.RefreshTokenExpired = DateTime.Now.AddHours(20);
        
        var roles = await _userManager.GetRolesAsync(user);
        var claim = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
        };
        foreach (var userRole in roles)
        {
            claim.Add(new Claim(ClaimTypes.Role, userRole));
        }
        
        var token = _tokenService.GerenateAcessToken(_configuration,claim);
        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            RefreshToken = newRefreshtoken,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expired = token.ValidTo
        });
    }
}