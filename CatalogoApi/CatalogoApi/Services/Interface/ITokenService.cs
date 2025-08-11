using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CatalogoApi.Services.Interface;

public interface ITokenService
{
    JwtSecurityToken GerenateAcessToken(IEnumerable<Claim> claims, IConfiguration _configuration);
    string GereateRefrashToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _configuration);
}