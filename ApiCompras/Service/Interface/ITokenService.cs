using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ApiCompras.Service.Interface;

public interface ITokenService
{
    SecurityToken GerenateAcessTokenJWt(IEnumerable<Claim> claims, IConfiguration configuration);
    string GerenateRefreshToken(IConfiguration configuration);
}