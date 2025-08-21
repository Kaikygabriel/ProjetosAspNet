using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlibiotecaApi.Model.DTO;
using BlibiotecaApi.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlibiotecaApi.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AuthController(IConfiguration configuration, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.configuration = configuration;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] LoginModel model)
    {
        var userExist = await userManager.FindByNameAsync(model.Name!);
        if(userExist is not null)
            return BadRequest();
        IdentityUser user = new IdentityUser()
        {
            UserName = model.Name,
            SecurityStamp =  Guid.NewGuid().ToString()
        };
        var result = await userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded)
            return NotFound("created is falid");
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByNameAsync(model.Name!);
        if (user is not null || await userManager.CheckPasswordAsync(user, model.Password!))
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.Name!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
              claims.Add(new Claim(ClaimTypes.Role , role));  
            }

            var token = TokenService.GerenateAcessToken(configuration, claims);

            return Ok(token);
        }

        return Unauthorized();
    }
}