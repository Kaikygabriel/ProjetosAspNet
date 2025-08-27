using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NotifiMe.Service.Interface;

namespace NotifiMe.Service;

public class TokenService: ITokenService 
{
    public ClaimsPrincipal GetPrincipalClaimsExpiredToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var princpal = tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
        }, out var secretToken);
        if(princpal is null)
            throw new Exception("Princpal is null");
        return princpal;
    }

    public string GerenateToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var bytesKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(bytesKey), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials = signingCredentials,
            Expires = DateTime.Now.AddHours(5),
            Subject = new ClaimsIdentity(claims)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GerenateRefreshToken()
    {
        var bytes = new byte[128];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}