using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using ProductsApi.Application.Services.Interfaces;

namespace ProductsApi.Application.Services;

public class TokenService : IServiceToken
{
    public string GerenateAcessToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    public string GerenateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal GetClaimsFromExpiredToken(string token, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }
}