using AutoMapper;
using FilmesApi.Models;
using FilmesApi.Models.DTO;

namespace FilmesApi.AutoMapper;

public class DomainToProfile : Profile
{
    public DomainToProfile()
    {
        CreateMap<Filme, FilmesDTO>().ReverseMap();
    }
}