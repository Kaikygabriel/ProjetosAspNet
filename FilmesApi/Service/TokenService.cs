using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FilmesApi.Service.Interface;
using Microsoft.IdentityModel.Tokens;

namespace FilmesApi.Service;

public class TokenService : ITokenService
{
    public string GerenateToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var key = configuration.GetSection("Jwt").GetValue<string>("SecretKey");
        var byteKey = Encoding.UTF8.GetBytes(key);
        var signingCredencials =
            new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(8),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials = signingCredencials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}