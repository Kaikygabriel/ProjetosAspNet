using Filmes.Application.DTOS;
using Filmes.Application.Interfaces;
using Filmes.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IServiceRepositoryUser _serviceRepositoryUser;

    public AuthController(IServiceRepositoryUser serviceRepositoryUser)
    {
        _serviceRepositoryUser = serviceRepositoryUser;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUserAsync(LoginModel model)
    {
        if (model is null)
            return NotFound();
        var user = await _serviceRepositoryUser.GetByName(model.Name!);
        if (user is not null || model.Password is null || model.Password!.Length < 6)
            return BadRequest();
        User userCreate = new()
        {
            Name = model.Name!,
            PasswordHash = model.Password!
        };
        await _serviceRepositoryUser.Create(userCreate);
        return NoContent();
    }
}