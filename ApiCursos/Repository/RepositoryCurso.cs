using APiCursos.Data;
using APiCursos.Model;
using ApiCursos.Repository.Interfaces;

namespace ApiCursos.Repository;

public class RepositoryCurso : Repository<Curso>,IRepositoryCurso
{
    public RepositoryCurso(ApiCursoContext context):base(context)
    {
        
    }
}