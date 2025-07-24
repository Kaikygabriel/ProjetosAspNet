using APiCursos.Model;
using APiCursos.Model.DTO;

namespace ApiCursos.ExtesionMethods;

public static class ConfigurationDTOCurso
{
    public static CursoDTO ToCursoDTO(this Curso curso)
    {
        if (curso is null)
            return null;
        return new CursoDTO()
        {
            Autor = curso.Autor,
            Id = curso.Id,
            Titulo = curso.Titulo
        };
    } 
    public static Curso ToCurso(this CursoDTO cursoDTO)
    {
        if (cursoDTO is null)
            return null;
        return new Curso()
        {
            DataLancamento = DateTime.Now,
            Autor = cursoDTO.Autor,
            Id = cursoDTO.Id,
            Titulo = cursoDTO.Titulo
        };
    }

    public static IEnumerable<CursoDTO> ToCursosDTOList(this IEnumerable<Curso> cursos)
    {
        if (cursos is null || !cursos.Any())
            return null;
        return cursos.Select(x => new CursoDTO()
        {
            Autor = x.Autor,
            Id = x.Id,
            Titulo = x.Titulo
        });
    }
}
    