using System.Text;
using Filmes.Application.Services;
using Filmes.Application.Services.Interfaces;
using Filmes.Domain.Interfaces;
using Filmes.Infraestruture.Data;
using Filmes.Infraestruture.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Filmes.Infraestruture.Extesions;

public  static class ExtesionService
{
    
    public static void ExtesionsServicesInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.RequireHttpsMetadata = false;
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            };
        });
        services.AddAuthorization();
        services.AddScoped<ITokenService,TokenService>();
        services.AddScoped<IFilmeServiceRepository,FilmeServiceRepository>();
    }
}