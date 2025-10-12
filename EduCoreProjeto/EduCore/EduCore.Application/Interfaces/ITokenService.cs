using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace EduCore.Application.Interfaces;

public interface ITokenService
{
    string GerenateRefreshToken();
    string GerenateAcessToken(IEnumerable<Claim>claims,IConfiguration configuration);
    ClaimsPrincipal GetClaimsFromExpiredToken(string token,IConfiguration configuration); 
}