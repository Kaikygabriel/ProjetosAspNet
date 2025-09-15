using Filmes.Domain.Interfaces;
using Filmes.Infraestruture.Data;
using Filmes.Infraestruture.Identity;
using Filmes.Infraestruture.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Filmes.Infraestruture.Extesions;

public static class ExtesionPersistent
{
    public static void ServiceExtesionsDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        services.AddScoped<IRepositoryFilme, RepositoryFilme>();
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
}