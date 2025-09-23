using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Catalogo.Application.Interfaces;

public interface ITokenService
{
    ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token,IConfiguration configuration);
    string GerenateRefreshToken();
    string GerenateJwt(IEnumerable<Claim> claims, IConfiguration configuration);
}