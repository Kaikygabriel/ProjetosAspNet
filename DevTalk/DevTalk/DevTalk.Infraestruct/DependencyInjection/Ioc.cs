using System.Text;
using DevTalk.Application.Service;
using DevTalk.Application.Service.Interfaces;
using DevTalk.Domain.BackOffice.Interfaces;
using DevTalk.Domain.BackOffice.Interfaces.User;
using DevTalk.Infraestruct.Repositories;
using DevTalk.Infraestruct.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DevTalk.Infraestruct.DependencyInjection;

public static class Ioc
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection app,IConfiguration configuration)
    {
        app.AddScoped<ITokenService,TokenService>();
        app.AddScoped<IUnitOfWork, UnitOfWork>();
        app.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        app.AddScoped<IRepositoryUser,RepositoryUser>();
        app.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.RequireHttpsMetadata = false;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
            };
        });
        app.AddAuthorization();
        return app;
    }   
}