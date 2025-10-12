
using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Commands.Update;

public class UpdateCourseCommand : IRequest<bool>
{
    public UpdateCourseCommand(Domain.Entities.Course course)
    {
        Course = course;
    }
    
    public Domain.Entities.Course Course { get; }
}