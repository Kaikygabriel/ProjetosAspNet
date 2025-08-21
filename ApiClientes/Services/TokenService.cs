using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApiClientes.Services.Interface;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ApiClientes.Services;

public class TokenService : ITokenService
{
    public JwtSecurityToken GerenateAcessToken(IConfiguration configure, IEnumerable<Claim> claims)
    {
        var key = configure.GetSection("JWT").GetValue<String>("SecretKey") ?? throw new Exception();
        var BytesKey = Encoding.UTF8.GetBytes(key);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(BytesKey), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Audience = configure.GetSection("JWT").GetValue<String>("ValidAudience"),
            Issuer = configure.GetSection("JWT").GetValue<String>("ValidIssuer"),
            Expires = DateTime.UtcNow.AddMinutes(
                configure.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
            SigningCredentials = signingCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.CreateJwtSecurityToken(tokenDescriptor) ;
    }

    public string GerenateRefreshToken()
    {
        var bytes = new byte[128];
        var gerenateNumber = RandomNumberGenerator.Create();
        gerenateNumber.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public ClaimsPrincipal ClaimExpiredToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false            
        },out var securityToken);
        return principal;
    }
}