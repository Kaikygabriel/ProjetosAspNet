using MediatorX.Core.Abstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProductsApi.Application.Dtos.User;
using ProductsApi.Application.Services.Interfaces;
using ProductsApi.Application.UseCases.User.Command.Create;
using ProductsApi.Application.UseCases.User.Query.GetByName;

namespace ProductsApi.Api.Controllers;

[EnableRateLimiting("Fixed")]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IServiceToken _serviceToken;
    private readonly IMediator _mediator;

    public AuthController(IServiceToken serviceToken, IMediator mediator, IConfiguration configuration)
    {
        _serviceToken = serviceToken;
        _mediator = mediator;
        _configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterAsync(UserCreate model)
    {
        if (model is null)
            return BadRequest("Model in login is invalid.");
        var userExist = await _mediator.SendAsync(new GetByNameUserQuery(model.Name));
        if (userExist is not null)
            return NotFound("Name in user existing.");
        var userCreate = model.ToUser();
        var resultCreateUser = await _mediator.SendAsync(new CreateUserCommand(userCreate));
        return resultCreateUser ? Created() : BadRequest("Error in create user!");
    }

    [HttpPost("Login")]
    public async Task<ActionResult> LoginAsync(UserLogin model)
    {
        if (model is null)
            return BadRequest("Model in login is invalid.");
        var user = await _mediator.SendAsync(new GetByNameUserQuery(model.Name));
        if (user is null || !user.CheckPassword(model.Password))
            return Unauthorized("User invalid or password invalid.");
        var claimsFromUser = _serviceToken.GetClaimsFromUser(user);
        var token = _serviceToken.GenerateAccessToken(claimsFromUser, _configuration);

        return Ok(new
        {
            Token = token
        });
    }
}   