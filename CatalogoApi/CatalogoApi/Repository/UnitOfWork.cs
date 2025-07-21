using CatalogoApi.Data;
using CatalogoApi.Repository.Interface;

namespace CatalogoApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(CatalogoContext context)
        {
            _context = context;
        }

        public CatalogoContext _context;
        private IRepositoryProduto? _produtoRepository;
        private IRepositoryCategoria? _categoriaRepository;

       public IRepositoryCategoria CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new RepositoryCategoria(_context);
            }
        }
        public IRepositoryProduto ProdutoRepository
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new RepositoryProduto(_context);
            }
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
