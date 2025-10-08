using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;

namespace EduCore.Application.Interfaces;

public interface ICourseServiceCache
{
    IUnitOfWork Unit { get; }
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> GetByTitle();
    Task CreateAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Course course);
}