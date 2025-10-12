using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Query.GetByTitle;

public class QueryByTitleCourse : IRequest<Domain.Entities.Course>
{
    public QueryByTitleCourse(string title)
    {
        Title = title;
    }
    public string Title { get; set; } = string.Empty;
}