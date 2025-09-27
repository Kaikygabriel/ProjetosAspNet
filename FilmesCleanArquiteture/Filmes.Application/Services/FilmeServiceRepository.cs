using System.Linq.Expressions;
using Filmes.Application.Interfaces;
using Filmes.Application.Services.Interfaces;
using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Filmes.Application.Services;

public class FilmeServiceRepository : IFilmeServiceRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;

    public FilmeServiceRepository(IUnitOfWork repository, IMemoryCache cache)
    {
        _unitOfWork= repository;
        _cache = cache;
    }

    public async Task<IEnumerable<Filme>> GetAll()
    {
        if (!_cache.TryGetValue("filmes", out IEnumerable<Filme> filmes))
        {
            filmes = await _unitOfWork.RepositoryFilme.GetAll();
            _cache.Set("filmes",filmes, new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromSeconds(60),
                Size = 1,
                Priority = CacheItemPriority.Normal
            });
        }

        return filmes;
    }

    public async Task<Filme?> GetByPredicate(Expression<Func<Filme,bool>>predicate)
    {
        return await _unitOfWork.RepositoryFilme.GetByPredicate(predicate);
    }

    public async Task Create(Filme entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _unitOfWork.RepositoryFilme.Create(entity);

        await _unitOfWork.CommitAsync();
    }

    public async Task Update(Filme entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _unitOfWork.RepositoryFilme.Update(entity);
        _cache.Remove("filmes");
        await _unitOfWork.CommitAsync();
    }

    public async Task Delete(Filme entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _unitOfWork.RepositoryFilme.Delete(entity);
        _cache.Remove("filmes");
        await _unitOfWork.CommitAsync();

    }
}