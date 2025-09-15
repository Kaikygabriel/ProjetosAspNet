using Catalogo.Domain.Interfaces;
using Catalogo.Infratructure.Context;
using Catalogo.Infratructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogo.CrossCuting.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Connection");
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseMySql(connection,ServerVersion.AutoDetect(connection)));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        services.AddScoped<IRepositoryProduto,RepositoryProduto>();
        services.AddScoped<IRepositoryCategoria,RepositoryCategoria>();

        return services;
    }
}