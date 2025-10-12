using EduCore.Application.Interfaces;
using EduCore.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EduCore.Application.Course;

public class CourseServiceCache : ICourseServiceCache
{
    public CourseServiceCache(IMemoryCache cache)
    {
        _cache = cache;
    }
    public CourseServiceCache(IUnitOfWork unit, IMemoryCache cache)
    {
        Unit = unit;
        _cache = cache;
    }

    private readonly IMemoryCache _cache;
    public IUnitOfWork Unit { get; }
    private const string NameCoursesInMemory = "Courses";
    public async Task<IEnumerable<Domain.Entities.Course>> GetAllAsync()
    {
        if (!_cache.TryGetValue(NameCoursesInMemory, out IEnumerable<Domain.Entities.Course>? courses))
        {
            courses = await Unit.RepositoryCourse.GetAllAsync();
            _cache.Set(NameCoursesInMemory, courses, new MemoryCacheEntryOptions()
            {
                Size = 1,
                SlidingExpiration = TimeSpan.FromMinutes(1),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                Priority = CacheItemPriority.Normal
            });
        }

        return courses;
    }

    public async Task<Domain.Entities.Course> GetByTitle(string title)
    {
        if (!_cache.TryGetValue($"Course-{title}", out Domain.Entities.Course? course))
        {
            course = await Unit.RepositoryCourse.GetByPredicateAsync(x => x.Title == title);
            _cache.Set($"Course-{title}", course, new MemoryCacheEntryOptions()
            {
                Size = 1,
                SlidingExpiration = TimeSpan.FromMinutes(1),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                Priority = CacheItemPriority.Normal
            });
        }

        return course;
    }

    public async Task CreateAsync(Domain.Entities.Course course)
    {
        Unit.RepositoryCourse.Create(course);
        await Unit.CommitAsync();
        _cache.Remove(NameCoursesInMemory);
    }

    public async Task UpdateAsync(Domain.Entities.Course course)
    {
        Unit.RepositoryCourse.Update(course);
        await Unit.CommitAsync();
        _cache.Remove(NameCoursesInMemory);
    }

    public async Task DeleteAsync(Domain.Entities.Course course)
    {
        Unit.RepositoryCourse.Delete(course);
        await Unit.CommitAsync();
        _cache.Remove(NameCoursesInMemory);
    }
}