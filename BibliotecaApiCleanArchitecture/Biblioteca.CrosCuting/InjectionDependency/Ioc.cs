using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Infraestructure.Context;
using Biblioteca.Infraestructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.CrosCuting.InjectionDependency;

public static class Ioc
{
    public static void AddServicesInjectionDependency(this IServiceCollection services,IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Connection");
        services.AddDbContext<AppDbContext>(x =>
            x.UseMySql(connection, ServerVersion.AutoDetect(connection)));
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRepositoryBook, RepositoryBook>();
        services.AddScoped<IRepositoryUser, RepositoryUser>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork,UnitOfWork>();
    }
}