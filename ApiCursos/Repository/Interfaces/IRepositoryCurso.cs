using APiCursos.Model;
using APiCursos.Pagination;

namespace ApiCursos.Repository.Interfaces;

public interface IRepositoryCurso : IRepository<Curso>
{
    Task<PagedList<Curso>> GetAllCursosAsync(CursoPagination pagination);
    
    Task<PagedList<Curso>> GetAllCursosByNameAsync(CursoFilterName pagination);
}