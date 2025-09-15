using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Filmes.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Filmes.Application.Services;

public class TokenService : ITokenService
{
    public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token, IConfiguration configuration)
    {
        var bytesKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(bytesKey)
        }, out var secretToken);
        
        if (secretToken is not JwtSecurityToken jwt || 
            !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token invalid");
        }
        return claimsPrincipal;
    }

    public string GerenateAcessToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Audience = configuration["Jwt:Audience"],
            Issuer = configuration["Jwt:Issuer"],
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(10),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GerenateAcessRefreshToken()
    {
        var bytes = new byte[128];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}