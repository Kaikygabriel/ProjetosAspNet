
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CatalogoApi.Model;
using CatalogoApi.Model.DTO;
using CatalogoApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CatalogoApi.Controllers;

[Route("[controller]")]
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
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };
            foreach (var UserRole in await _userManager.GetRolesAsync(user))
            {
                authClaims.Add(new Claim(ClaimTypes.Role,UserRole));
            }
            var token = _tokenService.GerenateAcessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GereateRefrashToken();
            int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], 
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

    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken([FromBody] TokenModel? model) 
    {
        if(model is null)
            return BadRequest("model is null");
        var acessToken = model.AccessToken ?? throw new Exception();
        var refreshToken = model.RefreshToken ?? throw new Exception();
        var principal = _tokenService.GetPrincipalFromExpiredToken(acessToken, _configuration);
        if (principal is null)
            return BadRequest("claims is null");
        var userName = principal.Identity.Name;
        var user = await _userManager.FindByNameAsync(userName!);
        if (user is null || user.RefreshToeken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid acess token refresh");
        var newAcessToken = _tokenService.GerenateAcessToken(principal.Claims.ToList(), _configuration);
        var newRefreshToken = _tokenService.GereateRefrashToken();

        user.RefreshToeken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            AcessToken = new JwtSecurityTokenHandler().WriteToken(newAcessToken),
            RefreshToken = newRefreshToken
        });
    }
    [Authorize]
    [HttpPost("Revoke/{userName:alpha}")]
    public async Task<ActionResult> Revoke(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            return NotFound();
        user.RefreshToeken = null;
        await _userManager.UpdateAsync(user);
        return NoContent();
    }

    [HttpPost("CreateRole/{roleName}")]
    public async Task<ActionResult> CreateRole([FromRoute] string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return BadRequest("O nome da role não pode ser vazio.");

        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists)
            return BadRequest($"A role '{roleName}' já existe.");

        var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (roleResult.Succeeded)
            return Ok($"Role '{roleName}' criada com sucesso.");

        return BadRequest("não de certo");
    }

    [HttpPost("AddUserToRole")]
    public async Task<ActionResult> addUserToRole(string name, string roleName)
    {
        var user = await _userManager.FindByNameAsync(name);
        if (user is null)
            return BadRequest();
        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
            return BadRequest();
        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
            return Ok("Success");
        return NotFound();
    } 
}