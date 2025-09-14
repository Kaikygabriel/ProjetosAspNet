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
        var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
        }, out var secretToken);
        if (claimsPrincipal is null)
            throw new NullReferenceException("The token are invalid");
        return claimsPrincipal;
    }

    public string GerenateToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var bytesKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(bytesKey), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(10),
            Audience = configuration["Jwt:Audience"],
            Issuer = configuration["Jwt:Issuer"],
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    

    public string GerenateRefreshToken()
    {
        var bytes = new byte[128];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}