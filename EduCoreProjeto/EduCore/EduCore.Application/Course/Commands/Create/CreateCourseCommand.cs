using EduCore.Domain.Entities;
using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Commands.Create;

public class CreateCourseCommand : IRequest<bool>
{
    public CreateCourseCommand(Domain.Entities.Course course)
    {
        Course = course;
    }
    
    public Domain.Entities.Course Course { get; }
}