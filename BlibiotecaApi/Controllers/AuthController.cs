using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlibiotecaApi.Model;
using BlibiotecaApi.Model.DTO;
using BlibiotecaApi.Service;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlibiotecaApi.Data;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> userManager;
    private readonly IConfiguration config;

    public AuthController(UserManager<User> userManager, IConfiguration config)
    {
        this.userManager = userManager;
        this.config = config;
    }

    [HttpPost("register")]
    public async Task<ActionResult> register([FromBody] LoginModel model)
    {
        var userExist = await userManager.FindByNameAsync(model.Name!);
        if (userExist is not null)
            return BadRequest();
        User user = new()
        {
            UserName = model.Name!,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded)
        {
            return NotFound();
        }
        return NoContent();
    }
    [HttpPost("login")]
    public async Task<ActionResult> login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByNameAsync(model.Name!);
        if (user is null || await userManager.CheckPasswordAsync(user, model.Password!))
            return NotFound();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,model.Name!)
        };
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(claims, BearerTokenDefaults.AuthenticationScheme)
        );
        return SignIn(claimsPrincipal);
    }
}