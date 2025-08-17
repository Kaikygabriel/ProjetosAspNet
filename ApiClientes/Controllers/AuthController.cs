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
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityUser> _userRole;

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName!);
        if (user is not null && await _userManager.CheckPasswordAsync(user, model.UserPassword!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };
            foreach (var userRole in userRoles)
            {
                claim.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GerenateAcessToken(_configuration, claim);
            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expired = token.ValidTo
            });
        }

        return Unauthorized();
    }
}