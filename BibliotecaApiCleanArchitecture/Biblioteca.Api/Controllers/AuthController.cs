using Biblioteca.Domain.BackOffice.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOf;

    public AuthController(IUnitOfWork unitOf)
    {
        _unitOf = unitOf;
    }
    
    
}