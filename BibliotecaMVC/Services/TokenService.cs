using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BibliotecaMVC.Services;

public static class TokenService
{
    public static string? GetNameFromToken(string token)
    {
        var tokenhandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("kjfalsdfkaJAKLJDkjçadsjfakljdfkkkFLÇA23");
        var claims = tokenhandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:5049",
            ValidAudience = "http://localhost:7256",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        }, out var securityToken);
        if(securityToken is not JwtSecurityToken )
            throw new Exception(" token is invalid");
        return claims.Identity?.Name!;
    }
}