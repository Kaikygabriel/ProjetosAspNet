using System.Security.Claims;
using Catalogo.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Catalogo.Application.Services;

public class TokenService : ITokenService
{
    public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    public string GerenateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public string GerenateJwt(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }
}