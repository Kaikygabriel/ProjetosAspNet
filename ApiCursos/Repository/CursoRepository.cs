using ApiCursos.Data;
using ApiCursos.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiCursos.Repository;

public class CursoRepository  : ICursoRepository
{
    private readonly ApiCursosContext _context;

    public CursoRepository(ApiCursosContext context)
    {
        _context = context;
    }
    public IEnumerable<Curso> GetCursos()
    {
        return _context.Cursos.Take(10).AsNoTracking().ToList();
    }

    public Curso? GetCurso(int id)
    {
        return _context.Cursos.AsNoTracking().SingleOrDefault(x => x.Id == id);
    }

    public Curso Create(Curso curso)
    {
        if (curso is null)
            throw new Exception(nameof(curso));
        _context.Cursos.Add(curso);
        _context.SaveChanges();
        return curso;
    }

    public Curso Update(Curso curso)
    {
        if (curso is null)
            throw new Exception(nameof(curso));
        _context.Cursos.Update(curso);
        _context.SaveChanges();
        return curso;
        
    }

    public Curso Delete(int id)
    {
        var curso = GetCurso(id);
        if (curso is null)
            throw new Exception(nameof(curso));
        _context.Cursos.Remove(curso);
        _context.SaveChanges();
        return curso;
    }
}