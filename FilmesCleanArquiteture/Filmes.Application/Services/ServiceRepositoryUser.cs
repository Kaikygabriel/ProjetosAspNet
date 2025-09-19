using System.Linq.Expressions;
using Filmes.Application.Interfaces;
using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Filmes.Application.Services;

public class ServiceRepositoryUser : IServiceRepositoryUser
{
    private readonly IMemoryCache _cache;
    private readonly IUnitOfWork _unitOf;

    public ServiceRepositoryUser(IUnitOfWork unitOf, IMemoryCache cache)
    {
        _unitOf = unitOf;
        _cache = cache;
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
    {
        if (!_cache.TryGetValue("Users", out IEnumerable<User>? users))
        {
            users = await _unitOf.RepositoryUser.GetAll(cancellationToken);
            _cache.Set("Users", users, new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(40),
                SlidingExpiration = TimeSpan.FromSeconds(10),
                Size = 1,
                Priority = CacheItemPriority.Normal
            });
        }

        return users;
    }

    public async Task<User> GetByPredicate(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _unitOf.RepositoryUser.GetByPredicate(predicate, cancellationToken);
    }

    public void Create(User entity)
    {
        _cache.Remove("Users");
        _unitOf.RepositoryUser.Create(entity);
    }

    public void Update(User entity)
    {
        _cache.Remove("Users");
        _unitOf.RepositoryUser.Update(entity);
    }

    public void Delete(User entity)
    {
        _cache.Remove("Users");
        _unitOf.RepositoryUser.Delete(entity);
    }
}