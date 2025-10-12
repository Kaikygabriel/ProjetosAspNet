using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EduCore.Application.DTOS.Student;
using EduCore.Application.Interfaces;
using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController: ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    
    public StudentsController(IUnitOfWork unitOfWork, ITokenService tokenService, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    private IEnumerable<Claim> GetClaimsFromUser(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        foreach (var role in user.GetRoles()!)
            claims.Add(new Claim(ClaimTypes.Role,role)); 
        return claims;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterAsync(RegisterStudentDTO model)
    {
        if (model is null)
            return BadRequest();
        var userExist = await _unitOfWork.RepositoryUser.GetByPredicateAsync(x => x.Name == model.Name);
        if (userExist is not null)
            return NotFound();

        User userCreate = new(model.Name,model.Password);
        Student studentCreate = new(userCreate, new Email(model.AdressEmail));
        userCreate.SetRoles("Student");
        
        _unitOfWork.RepositoryUser.Create(userCreate);
        _unitOfWork.RepositoryStudent.Create(studentCreate);

        await _unitOfWork.CommitAsync();
        return Created();
    }

    [HttpPost("Login")]
    public async Task<ActionResult> LoginAsync(LoginStudentDTO model)
    {
        if (model is null)
            return BadRequest();
        var user = await _unitOfWork.RepositoryUser.GetByPredicateAsync(x => x.Name == model.Name);
        if (user is null || !user.CheckPassword(model.Password)||
            !user.GetRoles()!.Exists(x=> x == "Student"))
            return NotFound();
        
        var claims = GetClaimsFromUser(user);

        var token = _tokenService.GerenateAcessToken(claims, _configuration);
        var refreshToken = _tokenService.GerenateRefreshToken();

        user.RefreshToken = refreshToken;
        user.ExpiredRefreshToken = DateTime.UtcNow.AddDays(4);
        
        _unitOfWork.RepositoryUser.Update(user);
        await _unitOfWork.CommitAsync();

        return Ok(new
        {
            token = token,
            refreshToken = refreshToken,
            expiredRefreshToken = DateTime.UtcNow.AddDays(4)
        });
    }
}