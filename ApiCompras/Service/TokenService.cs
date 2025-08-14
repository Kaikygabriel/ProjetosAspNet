using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApiCompras.Service.Interface;
using Microsoft.IdentityModel.Tokens;

namespace ApiCompras.Service;

public class TokenService  : ITokenService
{
    public SecurityToken GerenateAcessTokenJWt(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var key = configuration.GetSection("JWT").GetValue<string>("SecretKey");
        var bytesKey = Encoding.UTF8.GetBytes(key);
        var signingKeyBytes =new SigningCredentials(new SymmetricSecurityKey(bytesKey),SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Audience = configuration["JWT:ValidAudience"],
            Issuer = configuration["JWT:ValidIssuer"],
            Expires = DateTime.UtcNow.AddMinutes
                (configuration.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
            SigningCredentials = signingKeyBytes
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.CreateToken(tokenDescriptor);
    }

    public string GerenateRefreshToken(IConfiguration configuration)
    {
        var bytes = new byte[128];
        var numberRandom = RandomNumberGenerator.Create();
        numberRandom.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}