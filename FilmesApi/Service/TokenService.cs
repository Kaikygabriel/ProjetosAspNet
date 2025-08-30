using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FilmesApi.Service.Interface;
using Microsoft.IdentityModel.Tokens;

namespace FilmesApi.Service;

public class TokenService : ITokenService
{
    public string GerenateToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var Byteskey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Byteskey), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(10),
            SigningCredentials = signingCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal GetClaimsPrincipalExpiredToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
        }, out var securityToken);
        if (principal is null)
            throw new Exception("Token is invalid");
        return principal;
    }

    public string GerenateRefreshToken()
    {
        var bytes = new byte[128];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}