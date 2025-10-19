using System.Text;
using MediatorX.Core.DependencyInjection;
using ProductsApi.Infraestruct.Repositorys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProductsApi.Application.Services;
using ProductsApi.Application.Services.Interfaces;
using ProductsApi.Application.UseCases.User.Command.Create;
using ProductsApi.Domain.BackOffice.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces.Products;
using ProductsApi.Domain.BackOffice.Interfaces.Users;
using ProductsApi.Infraestruct.Repositorys.Product;
using ProductsApi.Infraestruct.Repositorys.User;

namespace ProductsApi.Infraestruct.DependencyInjection;

public static class Ioc
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services,IConfiguration config)
    {
        services.AddMediator(typeof(CreateUserCommand).Assembly);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.RequireHttpsMetadata = true;
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = config["Jwt:Audience"],
                ValidIssuer = config["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!))
            };
        });
        services.AddScoped<IUnitOfWork, ProductsApi.Infraestruct.Repositorys.UniOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRepositoryProduct, RepositoryProduct>();
        services.AddScoped<IRepositoryUser, RepositoryUser>();
        services.AddScoped<IServiceToken, TokenService>();
        return services;
    } 
}