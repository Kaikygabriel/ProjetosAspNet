using APiCursos.Data;
using APiCursos.Model;
using APiCursos.Pagination;
using ApiCursos.Repository.Interfaces;

namespace ApiCursos.Repository;

public class RepositoryCurso : Repository<Curso>,IRepositoryCurso
{
    public RepositoryCurso(ApiCursoContext context):base(context)
    {
    }

    public async Task<PagedList<Curso>> GetAllCursosAsync(CursoPagination pagination)
    {
        var cursos = await GetAllAsync();
        return PagedList<Curso>.ToPagedList(cursos,pagination.PageNumber,pagination.PageSize);
    }

    public async Task<PagedList<Curso>> GetAllCursosByNameAsync(CursoFilterName pagination)
    {
        var cursos =  await GetAllAsync();
        if (!string.IsNullOrEmpty(pagination.Name))
            cursos = cursos.Where(x => x.Titulo.Contains(pagination.Name));  
        return PagedList<Curso>.ToPagedList(cursos,pagination.PageNumber,pagination.PageSize);
    }
}