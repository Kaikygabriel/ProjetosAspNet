using LojaApi.Domain.BackOffice.Entitys;
using LojaApi.Domain.BackOffice.Interfaces;
using LojaApi.Domain.BackOffice.Interfaces.Category;
using LojaApi.Domain.BackOffice.Interfaces.Product;
using LojaApi.Infraestruct.Context;
using LojaApi.Infraestruct.Repository.Category;

namespace LojaApi.Infraestruct.Repository;

public class UnitOfWork : IUnitOfWork
{
    private CategoryRepository _repositoryCategory;
    private ProductRepository _repositoryProduct;
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }


    public IRepositoryCategory RepositoryCategory
        => _repositoryCategory ??= new CategoryRepository(_context);

    public IRepositoryProduct RepositoryProduct
        => _repositoryProduct ??= new ProductRepository(_context);

    public async Task CommitAsync()
    {
         await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}