
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CatalogoApi.Model;
using CatalogoApi.Model.DTO;
using CatalogoApi.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CatalogoApi.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(ITokenService tokenService, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _tokenService = tokenService;
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName!);
        if (user is not null && await _userManager.CheckPasswordAsync(user,model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };
            foreach (var UserRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role,UserRole));
            }

            var token = _tokenService.GerenateAcessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GereateRefrashToken();
            _=int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], 
                out int refreshTokenValidityInMinutes);
            user.RefreshToeken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(user);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterModel model)
    {
        var userExist = await _userManager.FindByNameAsync(model.UserName!);
        if (userExist is not null)
            return StatusCode(StatusCodes.Status500InternalServerError,"user already exists!");
        ApplicationUser user = new ApplicationUser() 
            { Email = model.Email, UserName = model.UserName,
                SecurityStamp= Guid.NewGuid().ToString() };
        var result =await _userManager.CreateAsync(user,model.Password!);
        if(!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError,"User creation failed");
        return Ok(new Response { Status = "Sucess", Message = "User created sucess!" });
    }
}