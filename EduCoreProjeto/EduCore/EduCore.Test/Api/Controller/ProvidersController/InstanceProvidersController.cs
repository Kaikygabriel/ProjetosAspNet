using EduCore.Application.Services;
using EduCore.Test.Mocks;
using Microsoft.Extensions.Configuration;

namespace EduCore.Test.Api.Controller.ProvidersController;

public static class InstanceProvidersController
{
    public static EduCore.Api.Controllers.ProvidersController CreateProviderController()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration configuration = builder.Build();

        return new EduCore.Api.Controllers.ProvidersController(new MockUnitOfWork(), new TokenService(),
            configuration);
    }  
}