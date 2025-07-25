using AutoMapper;
using CatalogoApi.Model;
using CatalogoApi.Model.DTO;

namespace CatalogoApi.AutoMapper
{
    public class DomationToProfileMapper : Profile
    {
        public DomationToProfileMapper()
        {
            CreateMap<Produto, ProdutoDTOUpdateRequest>().ReverseMap();
            CreateMap<Produto, ProdutoDTOUpdateResponse>().ReverseMap();
        }
    }
}
