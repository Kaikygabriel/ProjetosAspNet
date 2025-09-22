using CatalogoApi.Model;

namespace CatalogoApi.Services.Interface;

public interface IServiceCacheCategoria
{
    void SetCache<T>(string key, T data);
    void InvalidateCacheAfterChange(string categorias,int id, Categoria? categoria = null);
}