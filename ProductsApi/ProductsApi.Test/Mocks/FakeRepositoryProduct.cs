using System.Data;
using System.Linq.Expressions;
using ProductsApi.Domain.BackOffice.Entitys;
using ProductsApi.Domain.BackOffice.Interfaces.Products;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Test.Mocks;

public class FakeRepositoryProduct : IRepositoryProduct
{
    private List<Product> _products = new()
    {
        new Product(2999.99m, new Category("Eletrônicos"), "Notebook Dell Inspiron"),
        new Product(199.90m, new Category("Eletrônicos"), "Fone Bluetooth JBL"),
        new Product(89.90m, new Category("Roupa"), "Camiseta Nike"),
        new Product(159.90m, new Category("Roupa"), "Jaqueta de Couro"),
        new Product(49.90m, new Category("Livros"), "Clean Code"),
        new Product(39.90m, new Category("Livros"), "O Programador Pragmático"),
    };
    public Task<IEnumerable<Product>> GetAll()
    {
        return Task.FromResult<IEnumerable<Product>>(_products);
    }

    public async Task<Product?> GetByPredicate(Expression<Func<Product, bool>> predicate)
    {
        await Task.Delay(0);
        return _products.AsQueryable().FirstOrDefault(predicate);
    }

    public void Create(Product entity)
    {
        if (entity is null)
            throw new NoNullAllowedException();
        _products.Add(entity);
    }

    public void Update(Product entity)
    {
        if (entity is null)
            throw new NoNullAllowedException();
        _products.Add(entity);
    }

    public void Delete(Product entity)
    {
        if (entity is null)
            throw new NoNullAllowedException();
        _products.Remove(entity);
    }
}