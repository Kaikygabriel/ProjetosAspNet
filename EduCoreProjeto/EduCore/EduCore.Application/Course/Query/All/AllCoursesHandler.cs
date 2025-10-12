using EduCore.Application.Interfaces;
using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Query.All;

public class AllCoursesHandler(ICourseServiceCache cache) : 
                    IHandler<QueryAllCourses,IEnumerable<Domain.Entities.Course>>
{
    public async Task<IEnumerable<Domain.Entities.Course>> HandleAsync(QueryAllCourses request, CancellationToken cancellationToken = new CancellationToken())
    {
        return await cache.GetAllAsync();
    }
}