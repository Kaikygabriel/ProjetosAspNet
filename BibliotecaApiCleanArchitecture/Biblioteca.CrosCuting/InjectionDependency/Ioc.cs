using System.Text;
using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Infraestructure.Context;
using Biblioteca.Infraestructure.Repositorys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Biblioteca.CrosCuting.InjectionDependency;

public static class Ioc
{
    public static void AddServicesInjectionDependency(this IServiceCollection services,IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("connection");
        services.AddDbContext<AppDbContext>(x =>
            x.UseMySql(connection, ServerVersion.AutoDetect(connection)));
        services.AddScoped<IUnitOfWork,UnitOfWork>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRepositoryBook, RepositoryBook>();
        services.AddScoped<IRepositoryUser, RepositoryUser>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
            };
        });
        services.AddAuthorization();
    }
}