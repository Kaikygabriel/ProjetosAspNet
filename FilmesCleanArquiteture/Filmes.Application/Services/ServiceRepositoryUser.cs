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
    
    public async Task<User?> GetByName(string name)
    {
        if (!_cache.TryGetValue($"user-{name}", out User? user))
        {
            user = await _unitOf.RepositoryUser.GetByPredicate(x => x.Name.ToUpper().Trim() == name.ToUpper().Trim(),
                new CancellationToken());
            _cache.Set($"user{name}", user, new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(10),
                Priority = CacheItemPriority.Normal,
                Size = 1
            });
        }
        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        if (!_cache.TryGetValue($"users", out IEnumerable<User>? users))
        {
            users = await _unitOf.RepositoryUser.GetAll(new CancellationToken());
            _cache.Set($"users",users, new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(10),
                Priority = CacheItemPriority.Normal,
                Size = 1
            });
        }
        return users;
    }

    public async Task Create(User entity)
    {
        _unitOf.RepositoryUser.Create(entity);
        _cache.Remove($"user-{entity.Name}");
        await _unitOf.CommitAsync(new CancellationToken());
    }

    public async Task Update(User entity)
    {
        
        _unitOf.RepositoryUser.Update(entity);
        _cache.Remove($"user-{entity.Name}");
        await _unitOf.CommitAsync(new CancellationToken());
    }

    public async Task Delete(User entity)
    {
       
        _unitOf.RepositoryUser.Delete(entity);
        _cache.Remove($"user-{entity.Name}");
        await _unitOf.CommitAsync(new CancellationToken());
    }
}