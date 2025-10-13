namespace LojaApi.Infraestruct.Repository.Category
{
    using LojaApi.Domain.BackOffice.Entitys;
    using LojaApi.Domain.BackOffice.Interfaces;
    using LojaApi.Domain.BackOffice.Interfaces.Category;
    using LojaApi.Infraestruct.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class CategoryRepository : IRepositoryCategory
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _context.Categories.ToListAsync();

        public async Task<Category?> GetByPredicate(Expression<Func<Category, bool>> predicate) 
            => await _context.Categories.FirstOrDefaultAsync(predicate);

        public void Create(Category entity) 
            => _context.Categories.Add(entity);

        public void Update(Category entity) 
            => _context.Categories.Update(entity);

        public void Delete(Category entity) 
            => _context.Categories.Remove(entity);

    }
}