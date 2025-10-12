using EduCore.Application.Interfaces;
using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Commands.Create;

public class CreateCourseHandler(ICourseServiceCache _cache) : IHandler<CreateCourseCommand,bool>
{
    public async Task<bool> HandleAsync(CreateCourseCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            await _cache.CreateAsync(request.Course);
            return true;
        }
        catch(Exception e)
        {
            return true;
        }
    }
}