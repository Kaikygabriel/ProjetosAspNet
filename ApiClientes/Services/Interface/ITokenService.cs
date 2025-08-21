using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiClientes.Services.Interface;

public interface ITokenService
{
    JwtSecurityToken GerenateAcessToken(IConfiguration configure, IEnumerable<Claim> claims);
     ClaimsPrincipal ClaimExpiredToken(string token, IConfiguration configuration);
     public string GerenateRefreshToken();
}