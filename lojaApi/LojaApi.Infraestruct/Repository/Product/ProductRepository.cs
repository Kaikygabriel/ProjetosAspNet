namespace LojaApi.Infraestruct.Repository.Category
{
    using LojaApi.Domain.BackOffice.Entitys;
    using LojaApi.Domain.BackOffice.Interfaces;
    using LojaApi.Domain.BackOffice.Interfaces.Category;
    using LojaApi.Domain.BackOffice.Interfaces.Product;
    using LojaApi.Infraestruct.Context;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class ProductRepository : IRepositoryProduct
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() 
            => await _context.Products.ToListAsync();
        public async Task<Product?> GetByPredicate(Expression<Func<Product, bool>> predicate)  
            => await _context.Products.FirstOrDefaultAsync(predicate);

        public void Create(Product entity)
            => _context.Products.Add(entity);

        public void Update(Product entity)
             => _context.Products.Update(entity);

         public void Delete(Product entity)
             => _context.Products.Remove(entity);

    }
}