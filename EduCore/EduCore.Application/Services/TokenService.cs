using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EduCore.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EduCore.Application.Services;

public class TokenService : ITokenService
{
    public string GerenateRefreshToken()
    {
        var bytesArray = new byte[128];
        RandomNumberGenerator.Fill(bytesArray);
        return Convert.ToBase64String(bytesArray);
    }

    public string GerenateAcessToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var Key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        
    }

    public ClaimsPrincipal GetClaimsFromExpiredToken(string token, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }
    
}