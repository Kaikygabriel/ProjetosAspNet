using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.Application.Interfaces;

public interface ITokenService
{
    ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token,IConfiguration configuration);
    string GerenateAcessToken(IEnumerable<Claim> claims, IConfiguration configuration);
    string GerenateRefreshToken();
}
 