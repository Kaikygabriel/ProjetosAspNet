using System.Linq.Expressions;
using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Domain.ValueObjects;

namespace EduCore.Test.Mocks;

public class MockProviderRepository : IRepositoryProvider
{
    private readonly List<Provider> _providers = new();

    public MockProviderRepository()
    {
        _providers.Add(new Provider(
            new User { Name = "Kaiky", 
                       PasswordHash= BCrypt.Net.BCrypt.HashPassword("senhaSegura2") },
            new Email { Adress = "kaiky@example.com" }
        ));
            
        _providers.Add(new Provider(
            new User { Name = "Maria", 
                       PasswordHash = BCrypt.Net.BCrypt.HashPassword("senhaSegura") },
            new Email { Adress= "maria@example.com" }
        ));
    }

    public async Task<IEnumerable<Provider>> GetAllAsync()
    {
        await Task.Delay(0);
        return _providers;
    }

    public async Task<Provider?> GetByPredicateAsync(Expression<Func<Provider, bool>> predicate)
    {
        await Task.Delay(0);
        return _providers.AsQueryable().FirstOrDefault(predicate);
    }

    public void Create(Provider entity)
    {
        _providers.Add(entity);
    }

    public void Update(Provider entity)
    {
        _providers.Add(entity);
    }

    public void Delete(Provider entity)
    {
        _providers.Remove(entity);
    }

}