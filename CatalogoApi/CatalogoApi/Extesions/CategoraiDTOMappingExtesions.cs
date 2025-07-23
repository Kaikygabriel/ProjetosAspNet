using CatalogoApi.Model;
using CatalogoApi.Model.Dto;

namespace CatalogoApi.Extesions
{
    public static class CategoraiDTOMappingExtesions
    {
        public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria) 
        {
            if (categoria is null)
                return null;
            return new CategoriaDTO()
            {
                Id = categoria.Id,
                ImageUrl = categoria.ImageUrl,
                Nome = categoria.Nome
            };
        }
        public static Categoria? ToCategoria(this CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
                return null;
            return new Categoria()
            {
                Id = categoriaDto.Id,
                ImageUrl = categoriaDto.ImageUrl,
                Nome = categoriaDto.Nome
            };
        }
        public static IEnumerable<CategoriaDTO>? ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
        {
            if (categorias is null || !categorias.Any())
                return null;
            return categorias.Select(x=> new CategoriaDTO()
            {
                ImageUrl = x.ImageUrl,
                Nome = x.Nome,
                Id = x.Id
            }).ToList();
        }
    }
}
