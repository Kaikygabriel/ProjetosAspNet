using ApiConsultasMedicas.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiConsultasMedicas.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }
}