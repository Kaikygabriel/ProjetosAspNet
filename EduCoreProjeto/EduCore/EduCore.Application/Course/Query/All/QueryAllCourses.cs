using MediatorX.Core.Abstraction.Interfaces;

namespace EduCore.Application.Course.Query.All;

public class QueryAllCourses : IRequest<IEnumerable<Domain.Entities.Course>>;