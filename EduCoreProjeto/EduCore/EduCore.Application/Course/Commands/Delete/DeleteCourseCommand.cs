
using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Commands.Delete;

public class DeleteCourseCommand : IRequest<bool>
{
    public DeleteCourseCommand(Domain.Entities.Course course)
    {
        Course = course;
    }
    
    public Domain.Entities.Course Course { get; }
}