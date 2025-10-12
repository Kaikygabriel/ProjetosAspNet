using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;

namespace EduCore.Application.Interfaces;

public interface ICourseServiceCache
{
    IUnitOfWork Unit { get; }
    Task<IEnumerable<Domain.Entities.Course>> GetAllAsync();
    Task<Domain.Entities.Course> GetByTitle(string title);
    Task CreateAsync(Domain.Entities.Course course);
    Task UpdateAsync(Domain.Entities.Course course);
    Task DeleteAsync(Domain.Entities.Course course);
}