using System.Security.Claims;
using FilmesApi.Models;
using FilmesApi.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController  : ControllerBase
{
    private readonly ITokenService tokenService;
    private readonly IConfiguration configuration;
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AuthController(ITokenService tokenService, IConfiguration configuration, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.tokenService = tokenService;
        this.configuration = configuration;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }
    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] LoginModel? model)
    {
        if (model is null)
            return BadRequest();
        var userExist = await userManager.FindByNameAsync(model.Name);
        if (userExist is not null)
            return NotFound();
        IdentityUser user = new IdentityUser()
        {
            UserName = model.Name
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return NotFound("Falid in created");
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel? model)
    {
        if (model is null)
            return BadRequest();
        var user = await userManager.FindByNameAsync(model.Name);
        if (user is not null && await userManager.CheckPasswordAsync(user,model.Password))
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };
            foreach(var userRole in roles)
                claims.Add(new Claim(ClaimTypes.Role,userRole));
            var token = tokenService.GerenateToken(claims, configuration);
            return Ok(token);
        }
        return Unauthorized();
    }
}