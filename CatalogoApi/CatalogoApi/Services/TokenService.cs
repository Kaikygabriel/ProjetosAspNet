using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CatalogoApi.Services.Interface;
using Microsoft.IdentityModel.Tokens;

namespace CatalogoApi.Services;

public class TokenService : ITokenService
{
    public JwtSecurityToken GerenateAcessToken(IEnumerable<Claim> claims, IConfiguration _configuration)
    {
        var key = _configuration.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new Exception();
        var bytesKey = Encoding.UTF8.GetBytes(key);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(bytesKey), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetSection("JWT")
                .GetValue<double>("TokenValidityInMinutes")),
            Audience = _configuration.GetSection("JWT").GetValue<string>("ValidAudience"),
            Issuer = _configuration.GetSection("JWT").GetValue<string>("ValidIssuer"),
            SigningCredentials = signingCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
    }

     public string GereateRefrashToken()
     {
         var secureRandomBytes = new byte[128];
         using var randomNumberGerenate = RandomNumberGenerator.Create();
         randomNumberGerenate.GetBytes(secureRandomBytes);
         var refreshToken = Convert.ToBase64String(secureRandomBytes);
         return refreshToken;
     }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _configuration)
    {
        var key = _configuration["JWT:SecretKey"];
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityTokena ||
            !jwtSecurityTokena.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new Exception("Token is not validity");
        }            
        return principal;
    }
} 