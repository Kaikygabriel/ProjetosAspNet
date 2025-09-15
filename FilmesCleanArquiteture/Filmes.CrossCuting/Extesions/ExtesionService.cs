using Filmes.Application.Services;
using Filmes.Application.Services.Interfaces;
using Filmes.Domain.Interfaces;
using Filmes.Infraestruture.Data;
using Filmes.Infraestruture.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Filmes.Infraestruture.Extesions;

public  static class ExtesionService
{
    
    public static void ExtesionsServicesInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService,TokenService>();
    }
}