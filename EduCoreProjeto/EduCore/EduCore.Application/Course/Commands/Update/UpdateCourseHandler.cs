using EduCore.Application.Interfaces;
using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Commands.Update;

public class UpdateCourseHandler(ICourseServiceCache _cache) : IHandler<UpdateCourseCommand,bool> 
{
    public async Task<bool> HandleAsync(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _cache.UpdateAsync(request.Course);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}