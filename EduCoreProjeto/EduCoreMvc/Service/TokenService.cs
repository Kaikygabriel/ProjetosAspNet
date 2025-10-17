using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EduCoreMvc.Service;

public static class TokenService
{
    public static ClaimsPrincipal GetClaimsFromToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateLifetime = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["Jwt:Secretkey"]!))
        }, out var JwtToken);
        if (JwtToken is not JwtSecurityToken)
            throw new Exception();
        return claims;
    } 
}