using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EduCore.Application.Course;
using EduCore.Application.Interfaces;
using EduCore.Application.Services;
using EduCore.Application.Course.Commands.Create;
using EduCore.Application.Course.Commands.Delete;
using EduCore.Application.Course.Commands.Update;
using EduCore.Application.Course.Query.All;
using EduCore.Application.Course.Query.GetByTitle;
using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Infraestruct.Context;
using EduCore.Infraestruct.Repositorys;
using MediatorX.Core.Abstraction.Interfaces;
using MediatorX.Core.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace EduCore.CrossCuting.ID;

public static class Ioc
{
    public  static IServiceCollection  AddDependencyInjectionApplication(this IServiceCollection service
                                                        ,IConfiguration configuration)
    {

        service.AddMediator(typeof(CreateCourseHandler).Assembly);
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IRepositoryUser,RepositoryUser>();
        service.AddScoped<IRepositoryCourse,RepositoryCourse>();
        service.AddScoped<IRepositoryProvider,RepositoryProvider>();
        service.AddScoped<IRepositoryStudent,RepositoryStudent>();
        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<ICourseServiceCache,CourseServiceCache>();

        var connection = configuration.GetConnectionString("DefaulsConnection");
        service.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

        service.AddAuthentication(x =>
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
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
            };
        });
        service.AddAuthorization(x =>
        {
            x.AddPolicy("StudentOnly", options
                => options.RequireRole("Student"));
            x.AddPolicy("ProviderOnly",options 
                => options.RequireRole("Provider"));
        });

        return service;
    }
}