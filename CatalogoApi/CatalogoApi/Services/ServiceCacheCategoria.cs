using CatalogoApi.Controllers;
using CatalogoApi.Model;
using CatalogoApi.Services.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogoApi.Services;

public class ServiceCacheCategoria : IServiceCacheCategoria
{
 
    private readonly IMemoryCache _cache;

    public ServiceCacheCategoria(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    private string GetCategoriaCacheKey(int id)
        => $"Categoria{id}";
    
     public void InvalidateCacheAfterChange(string categorias,int id, Categoria? categoria = null)
    {
        _cache.Remove(categorias);
        _cache.Remove(GetCategoriaCacheKey(id));
        if (categoria is not null)
            SetCache<Categoria>(GetCategoriaCacheKey(id),categoria);
    }
    
    public void SetCache<T>(string key, T data)
    {
        _cache.Set(key, data, new MemoryCacheEntryOptions()
        {
            Size = 1,
            Priority = CacheItemPriority.Normal,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(10)
        });
    }
}