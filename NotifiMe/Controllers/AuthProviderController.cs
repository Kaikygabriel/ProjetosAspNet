using Microsoft.AspNetCore.Mvc;
using NotifiMe.Models;
using NotifiMe.Models.LoginModel;
using NotifiMe.Repository.Interface;
using NotifiMe.Service.Interface;

namespace NotifiMe.Controllers;

[ApiController]
[Route("auth/provider")]
public class AuthProviderController : ControllerBase
{
    
    private readonly ITokenService tokenService;
    private readonly IUnitOfWork _uwf;
    private readonly IConfiguration configuration;

    public AuthProviderController(ITokenService tokenService, IUnitOfWork uwf, IConfiguration configuration)
    {
        this.tokenService = tokenService;
        _uwf = uwf;
        this.configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> register([FromBody]LoginProviderModel model)
    {
        if (model is null)
            return NotFound();
        var userExist = await _uwf.ProviderRepository.GetByIdAsync(x => x.Name == model.Name!);
        if (userExist is not null)
            return NotFound();
        Provider user = new()
        {
            Name = model.Name,
            PasswordHash = model.Password,
            Email = model.Email,
            Work = model.Work
        };
        
        _uwf.ProviderRepository.Create(user);
        await _uwf.CommitAsync();
        
        return NoContent();
    }
}