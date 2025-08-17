using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiCursos.Model;
using ApiCursos.Repository;
using Microsoft.IdentityModel.Tokens;

namespace ApiCursos.Service;

public class TokenService
{
    
    

    public string GerenateToken(LoginModel model, IConfiguration configuration)
    {

            var claims = new[]
            {
             new Claim(ClaimTypes.Name, model.Name!),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!)), SecurityAlgorithms.HmacSha256),
                Issuer = configuration["JWT:ValidIssuer"],
                Audience = configuration["JWT:ValidAudience"]   ,         
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);   
    }

}