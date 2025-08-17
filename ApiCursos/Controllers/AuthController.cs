using ApiCursos.Model;
using ApiCursos.Repository;
using ApiCursos.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiCursos.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly RepositoryUsers repositoryUsers;
    private readonly IConfiguration configuration;

    public AuthController(RepositoryUsers repositoryUsers, TokenService tokenService, IConfiguration configuration)
    {
        this.repositoryUsers = repositoryUsers;
        _tokenService = tokenService;
        this.configuration = configuration;
    }
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginModel model)
    {
        var user = repositoryUsers.Get(model.Name!);
        if (user is not null)
        {
            var token = _tokenService.GerenateToken(model, configuration);
            return Ok(new
            {
                Token = token
            });
        }
        return Unauthorized();
    }
}