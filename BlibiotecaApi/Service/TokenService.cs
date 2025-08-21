using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BlibiotecaApi.Service;

public static class TokenService
{
    public static string GerenateAcessToken(IConfiguration configuration, IEnumerable<Claim> claims)
    {
        var key = configuration["Jwt:SecretKey"];
        var byteKey = Encoding.UTF8.GetBytes(key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(8),
            Subject = new ClaimsIdentity(claims),
            Audience = configuration["Jwt:Audience"],
            Issuer = configuration["Jwt:Issuer"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey),
                SecurityAlgorithms.HmacSha256)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}