// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using BlibiotecaApi.Model;
// using BlibiotecaApi.Model.DTO;
// using BlibiotecaApi.Service;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;

// namespace BlibiotecaApi.Controller;

// [ApiController]
// [Route("[controller]")]
// public class AuthController : ControllerBase
// {
//     private readonly IConfiguration configuration;
//     private readonly UserManager<User> userManager;
//     private readonly RoleManager<IdentityRole> roleManager;

//     public AuthController(IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
//     {
//         this.configuration = configuration;
//         this.userManager = userManager;
//         this.roleManager = roleManager;
//     }

//     [HttpPost("register")]
//     public async Task<ActionResult> Register([FromBody] LoginModel model)
//     {
//         var userExist = await userManager.FindByNameAsync(model.Name!);
//         if(userExist is not null)
//             return BadRequest();
//         User user = new User()
//         {
//             UserName = model.Name,
//             SecurityStamp =  Guid.NewGuid().ToString()
//         };
//         var result = await userManager.CreateAsync(user, model.Password!);
//         if (!result.Succeeded)
//             return NotFound("created is falid");
//         return Ok();
//     }

//     [HttpPost("Login")]
//     public async Task<ActionResult> Login([FromBody] LoginModel model)
//     {
//         var user = await userManager.FindByNameAsync(model.Name!);
//         if (user is not null && await userManager.CheckPasswordAsync(user, model.Password!))
//         {
//             var claims = new List<Claim>()
//             {
//                 new Claim(ClaimTypes.Name, model.Name!),
//                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//             };
//             foreach (var role in await userManager.GetRolesAsync(user))
//             {
//               claims.Add(new Claim(ClaimTypes.Role , role));  
//             }
            
//             var token = TokenService.GerenateAcessToken(configuration, claims);
//             var refreshToken = TokenService.GerenateRefreshToken();

//             user.RefreshToken = refreshToken;
//             user.ExpiredRefreshToken = DateTime.Now.AddHours(10);

//             await userManager.UpdateAsync(user);
            
//             return Ok(new
//             {
//                 Token =token,
//                 RefreshToken = refreshToken
//             });
//         }
//         return Unauthorized();
//     }

//     [HttpPost("Refresh")]
//     public async Task<ActionResult> Refresh([FromBody] TokenLogin model)
//     {
//         var acessToken = model.Token ?? throw new Exception();
//         var acessRefreshToken = model.RefreshToken?? throw new Exception();

//         var principal = TokenService.GetClaimsPrincipalInExpiredToken(acessToken,configuration);
//         if (principal is null)
//             return NotFound();

//         var userName = principal.Identity!.Name;
//         var user = await userManager.FindByNameAsync(userName!);
//         if (user is null || user.RefreshToken != acessRefreshToken || user.ExpiredRefreshToken <= DateTime.Now)
//             return BadRequest();

//         var newRefreshToken = TokenService.GerenateRefreshToken();
//         var newToken = TokenService.GerenateAcessToken( configuration,principal.Claims.ToList());

//         user.RefreshToken = newRefreshToken;
//         user.ExpiredRefreshToken = DateTime.Now.AddHours(10);

//         await userManager.UpdateAsync(user);
//         return Ok(new
//         {
//             Token = newToken,
//             RefreshToken = newRefreshToken
//         });
//     }
// }