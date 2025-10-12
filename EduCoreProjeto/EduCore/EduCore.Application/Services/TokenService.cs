using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EduCore.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
        var keyBytes = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddDays(3),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal GetClaimsFromExpiredToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyBytes = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        }, out var securityToken);
        if (securityToken is not JwtSecurityToken tokenValid)
            throw new Exception("Token is invalid!");
        return claimsPrincipal;
    }
    
}