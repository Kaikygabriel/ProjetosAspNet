using EduCore.Application.Interfaces;
using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Commands.Delete;

public class DeleteCourseHandler(ICourseServiceCache _cache) : IHandler<DeleteCourseCommand,bool>
{

    public async Task<bool> HandleAsync(DeleteCourseCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            await _cache.DeleteAsync(request.Course);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}