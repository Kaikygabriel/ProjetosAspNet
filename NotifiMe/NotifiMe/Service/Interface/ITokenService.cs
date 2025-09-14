using System.Security.Claims;

namespace NotifiMe.Service.Interface;

public interface ITokenService
{
    ClaimsPrincipal GetPrincipalClaimsExpiredToken(string token,IConfiguration configuration);
    string GerenateToken(IEnumerable<Claim> claims, IConfiguration configuration);
    string GerenateRefreshToken();
}