using DevTalk.Application.Service;
using DevTalk.Application.Service.Interfaces;
using DevTalk.Domain.BackOffice.Interfaces;
using DevTalk.Domain.BackOffice.Interfaces.User;
using DevTalk.Infraestruct.Repositories;
using DevTalk.Infraestruct.Repositories.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevTalk.Infraestruct.DependencyInjection;

public static class Ioc
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection app,IConfiguration configuration)
    {
        app.AddScoped<ITokenService,TokenService>();
        app.AddScoped<IUnitOfWork, UnitOfWork>();
        app.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        app.AddScoped<IRepositoryUser,RepositoryUser>();
        return app;
    }   
}