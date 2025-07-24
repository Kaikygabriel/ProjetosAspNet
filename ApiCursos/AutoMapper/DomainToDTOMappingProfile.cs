using APiCursos.Model;
using APiCursos.Model.DTO;
using AutoMapper;

namespace APiCursos.AutoMapper;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Curso, CursoDTO>().ReverseMap();
    }
}