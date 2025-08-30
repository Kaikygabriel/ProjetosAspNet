using System.Security.Claims;

namespace FilmesApi.Service.Interface;

public interface ITokenService
{
    string GerenateToken(IEnumerable<Claim> claims, IConfiguration configuration);
    ClaimsPrincipal GetClaimsPrincipalExpiredToken(string token, IConfiguration configuration);
    string GerenateRefreshToken();
}