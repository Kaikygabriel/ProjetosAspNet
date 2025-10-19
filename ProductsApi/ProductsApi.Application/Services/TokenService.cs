using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ProductsApi.Application.Services.Interfaces;
using ProductsApi.Domain.BackOffice.Entitys;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace ProductsApi.Application.Services;

public class TokenService : IServiceToken
{
    public string GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var creadentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Expires = DateTime.Now.AddDays(3),
            Subject = new ClaimsIdentity(claims),
            Audience = configuration["Jwt:Audience"],
            Issuer = configuration["Jwt:Issuer"],
            SigningCredentials = creadentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public IEnumerable<Claim> GetClaimsFromUser(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email.Address),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        foreach (var role in user.GetRoles())
            claims.Add(new Claim(ClaimTypes.Role,role));
        return claims;
    }
}