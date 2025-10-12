using EduCore.Application.Interfaces;
using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Query.GetByTitle;

public class CourseByTitleHandler(ICourseServiceCache cache) : IHandler<QueryByTitleCourse,Domain.Entities.Course>
{
    public async Task<Domain.Entities.Course> HandleAsync(QueryByTitleCourse request, CancellationToken cancellationToken = new CancellationToken())
    {
        return await cache.GetByTitle(request.Title);
    }
}