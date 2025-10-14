using LojaApi.Application.UseCases.Category.Commmands.Create;
using LojaApi.Domain.BackOffice.Interfaces;
using LojaApi.Domain.BackOffice.Interfaces.Category;
using LojaApi.Domain.BackOffice.Interfaces.Product;
using LojaApi.Infraestruct.Repository;
using LojaApi.Infraestruct.Repository.Category;
using MediatorX.Core.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LojaApi.Infraestruct.DependencyInjection;

public static class Ioc
{
    public static IServiceCollection AddDepencyInjection(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        services.AddMediator(typeof(CreateCategoryRequest).Assembly);
        services.AddScoped<IRepositoryCategory, CategoryRepository>();
        services.AddScoped<IRepositoryProduct, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}