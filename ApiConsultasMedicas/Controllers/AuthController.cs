using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiConsultasMedicas.Model;
using ApiConsultasMedicas.Models;
using ApiConsultasMedicas.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiConsultasMedicas.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly IConfiguration configuration;
    private readonly UserManager<User> _userManager;
    public AuthController(TokenService tokenService, UserManager<User> userManager, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        this.configuration = configuration;
    }
    [HttpPost("register")]
    public async Task<ActionResult> register([FromBody] LoginModel model)
    {
        if (model is null)
            return NotFound();
        var userExist = await _userManager.FindByNameAsync(model.Name!);
        if (userExist is not null)
            return NotFound();
        User user = new User
        {
            UserName = model.Name,
            SecurityStamp = Guid.NewGuid().ToString("N")
        };
        var result = await _userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded)
            return BadRequest();
        return Ok();
    }
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        if (model is null)
            return NotFound();
        var user = await _userManager.FindByNameAsync(model.Name!);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N"))
            };
            foreach (var role in await _userManager.GetRolesAsync(user))
                claims.Add(new Claim(ClaimTypes.Role, role));
            var token = _tokenService.GerenateToken(claims, configuration);
            var refreshToken = _tokenService.GerenateRefreshToken();
            user.RefreshToken = refreshToken;
            user.ExpiredRefreshToken = DateTime.Now.AddHours(10);
            await _userManager.UpdateAsync(user);
            return Ok(new
            {
                token = token,
                RefreshToken = refreshToken
            }
            );
        }
        return Unauthorized();
    }
    [HttpPost("Refresh")]
    public async Task<ActionResult> RefreshToken([FromBody] LoginToken model)
    {
        if (model is null) return NotFound();

        var principal = _tokenService.GetClaimInToken(model.Token!, configuration);
        if (principal == null) return BadRequest();

        var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);
        if (user is null) return NotFound();

        if (user.RefreshToken != model.RefreshToken || user.ExpiredRefreshToken <= DateTime.Now)
            return Unauthorized();

        var newRefreshToken = _tokenService.GerenateRefreshToken();
        var newToken = _tokenService.GerenateToken(principal.Claims.ToList(), configuration);

        user.RefreshToken = newRefreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(10);

        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            token = newToken,
            refreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpPost("Revoke/{userName:Alpha}")]
    public async Task<ActionResult> Revoke([FromRoute] string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return NotFound();
        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
        return NoContent();
    }
}