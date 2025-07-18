using ApiCursos.Model;

namespace ApiCursos.Repository;

public interface ICursoRepository
{
    IEnumerable<Curso> GetCursos();
    Curso GetCurso(int id);
    Curso Create(Curso curso);
    Curso Update(Curso curso);
    Curso Delete(int id);
}