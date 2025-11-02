using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevTalk.Application.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevTalk.Application.Service;

public class TokenService : ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var credential = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        { 
            Expires = DateTime.UtcNow.AddDays(3),
            Subject = new ClaimsIdentity(claims),
            Audience = configuration["Jwt:Audience"],
            Issuer = configuration["Jwt:Issuer"],
            SigningCredentials = credential
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}