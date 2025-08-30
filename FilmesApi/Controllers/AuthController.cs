using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FilmesApi.Models;
using FilmesApi.Models.DTO;
using FilmesApi.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginModel = FilmesApi.Models.LoginModel;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService tokenService;
    private readonly IConfiguration configuration;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AuthController(ITokenService tokenService, IConfiguration configuration, 
                          UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.tokenService = tokenService;
        this.configuration = configuration;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] LoginModel model)
    {
        if (model is null)
            return BadRequest();
        var userExist = await userManager.FindByNameAsync(model.Name!);
        if (userExist is not null)
            return NotFound();
        User user = new()
        {
            UserName = model.Name,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await userManager.CreateAsync(user, model.Password!);
        if (result.Succeeded)
            return NoContent();
        return NotFound();
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByNameAsync(model.Name ?? throw new Exception());
        if (user is null)
            return NotFound();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        foreach(var role in await userManager.GetRolesAsync(user))
            claims.Add(new Claim(ClaimTypes.Role,role));

        var token = tokenService.GerenateToken(claims,configuration);
        var refreshToken = tokenService.GerenateRefreshToken();

        user.RefreshToken = refreshToken;
        user.ExpiredRefreshToken = DateTime.Now.AddHours(10);

        await userManager.UpdateAsync(user);

        return Ok(new
        {
            token = token,
            refreshToken= refreshToken,
            ExpitedRefreshToken =DateTime.Now.AddHours(10)
        });
    }

    [HttpPost("CreateRole")]
    public async Task<ActionResult> CreateRole([FromBody] string roleName)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (roleExist)
            return NotFound();
        var result = await roleManager.CreateAsync(new IdentityRole(roleName));
        if (result.Succeeded)
            return Ok();
        return NotFound();
    }

    [HttpPost("AddRoleInUser")]
    public async Task<ActionResult> AddRoleToUser([FromBody]LoginAddRoleInUser model)
    {
        var user = await userManager.FindByNameAsync(model.UserName ?? throw new Exception());
        if (user is null)
            return NotFound();
        var roleExist = await roleManager.RoleExistsAsync(model.Role);
        if (!roleExist)
            return NotFound();
        var role = await roleManager.FindByNameAsync(model.Role);

        var result = await userManager.AddToRoleAsync(user, model.Role);
        if (result.Succeeded)
            return Ok();
        return BadRequest();
    }
}