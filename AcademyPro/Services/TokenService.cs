using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
    public static string GerenateRefreshToken()
    {
        var bytes = new Byte[128];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
    public static ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var KeyBytes = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var ClaimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateLifetime = false,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
        }, out var securityToken);
        if (ClaimsPrincipal is null)
            throw new ArgumentNullException(nameof(ClaimsPrincipal));
        return ClaimsPrincipal;
    }
}