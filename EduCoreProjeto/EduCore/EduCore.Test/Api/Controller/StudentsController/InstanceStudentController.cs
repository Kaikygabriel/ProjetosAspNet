using EduCore.Application.Services;
using EduCore.Test.Mocks;
using Microsoft.Extensions.Configuration;

namespace EduCore.Test.Api.Controller.StudentsController;

public static class InstanceStudentController
{
    public static EduCore.Api.Controllers.StudentsController CreateProviderController()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration configuration = builder.Build();

        return new EduCore.Api.Controllers.StudentsController(new MockUnitOfWork(), new TokenService(),
            configuration);
    }  
}