using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AcademyPro.Services;

public static class TokenService
{
    public static string GerenateAcessToken(IConfiguration configuration, IEnumerable<Claim> claims)
    {
        var keyBytes = Encoding.UTF8.GetBytes((configuration["Jwt:SecretKey"]!));
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Audience = configuration["Jwt:Audience"],
            Issuer = configuration["Jwt:Issuer"],
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(10),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}