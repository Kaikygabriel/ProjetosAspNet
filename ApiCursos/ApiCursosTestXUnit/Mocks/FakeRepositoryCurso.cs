using System.Linq.Expressions;
using APiCursos.Model;
using APiCursos.Pagination;
using ApiCursos.Repository.Interfaces;

namespace ApiCursosTestXUnit.Mocks;

public class FakeRepositoryCurso : IRepositoryCurso
{
    public List<Curso> cursos = new()
    {
        new Curso() { Autor = "Kakiky", DataLancamento = DateTime.Now, Id = 1, Titulo = "teste kaiky" },
        new Curso() { Autor = "Alves", DataLancamento = DateTime.Now, Id = 2, Titulo = "teste Alves" }
    };
    public async Task<IEnumerable<Curso>> GetAllAsync()
    {
        await Task.Delay(0);
        return cursos;
    }

    public async Task<Curso?> GetByIDAsync(Expression<Func<Curso,bool>> predicate)
    {
        await Task.Delay(0);
        return cursos.AsQueryable().SingleOrDefault(predicate);
    }

    public async Task<Curso> CreateAsync(Curso entity)
    {
        await Task.Delay(0);
        cursos.Add(entity);
        return entity;
    }

    public Curso Update(Curso entity)
    {
        cursos.Add(entity);
        return entity;
    }

    public Curso Delete(Curso entity)
    {
        cursos.Remove(entity);
        return entity;
    }

    public async Task<PagedList<Curso>> GetAllCursosAsync(CursoPagination pagination)
    {
        await Task.Delay(0);
        var paged = PagedList<Curso>.ToPagedList(cursos, pagination.PageNumber, pagination.PageSize);
        return paged;
    }

    public async Task<PagedList<Curso>> GetAllCursosByNameAsync(CursoFilterName pagination)
    {
        await Task.Delay(0);
        var paged = PagedList<Curso>.ToPagedList(cursos, pagination.PageNumber, pagination.PageSize);
        return paged;
    }
}