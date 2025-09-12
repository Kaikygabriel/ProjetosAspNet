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
        var bytesKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(bytesKey), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Audience = configuration["Jwt:Audience"],
            Issuer = configuration["Jwt:Issuer"],
            SigningCredentials = credentials,
            Expires = DateTime.Now.AddHours(10)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal GetClaimsPrincipalExpiredToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidIssuer = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))},
                out var secretToken);
        if (secretToken is not JwtSecurityToken)
            throw new Exception();
        return claimsPrincipal;
    }

    public string GerenateRefreshToken()
    {
        var bytes = new byte[128];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}