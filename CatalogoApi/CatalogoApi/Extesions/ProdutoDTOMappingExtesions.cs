using System.Runtime.CompilerServices;
using CatalogoApi.Model;
using CatalogoApi.Model.Dto;
using CatalogoApi.Model.DTO;

namespace CatalogoApi.Extesions
{
    public static class ProdutoDTOMappingExtesions
    {
        public static ProdutoDTO? ToProdutoDTO(this Produto produto)
        {
            if (produto is null)
                return null;
            return new ProdutoDTO()
            {
                CategoriaId = produto.CategoriaId,
                Descricao = produto.Descricao,
                Id = produto.Id,
                ImagemUrl = produto.ImagemUrl,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }
        public static Produto? ToProduto(this ProdutoDTO produtoDTO)
        {
            if (produtoDTO is null)
                return null;
            return new Produto()
            {
                CategoriaId = produtoDTO.CategoriaId,
                Descricao = produtoDTO.Descricao,
                Id = produtoDTO.Id,
                ImagemUrl = produtoDTO.ImagemUrl,
                Nome = produtoDTO.Nome,
                Preco = produtoDTO.Preco
            };
        }
        public static IEnumerable<ProdutoDTO>? ToProdutosDTOList(this IEnumerable<Produto> produtos)
        {
            if (produtos is null || !produtos.Any())
                return null;
            return produtos.Select(x=>new ProdutoDTO()
            { 
                CategoriaId=x.CategoriaId,
                Descricao=x.Descricao,
                Id=x.Id,
                ImagemUrl=x.ImagemUrl,
                Nome=x.Nome,
                Preco=x.Preco
            });
        }
        public static Produto ?ToProduto(this ProdutoDTOUpdateRequest produtoDTOUpdateRequest)
        {
            if (produtoDTOUpdateRequest is null)
                return null;
            return new Produto
            {
                DataCadastro = produtoDTOUpdateRequest.DataCadastro,
                Estoque = produtoDTOUpdateRequest.Estoque
            };
        }
        public static ProdutoDTOUpdateRequest? ToProdutoDtoUpdateRequest(this Produto produto)
        {
            if (produto is null)
                return null;
            return new ProdutoDTOUpdateRequest
            {
                DataCadastro = produto.DataCadastro,
                Estoque = produto.Estoque
            };
        }
        public static Produto? ToProduto(this ProdutoDTOUpdateResponse produtoDTOUpdateResponse)
        {
            if (produtoDTOUpdateResponse is null)
                return null;
            return new Produto
            {
              Estoque= produtoDTOUpdateResponse.Estoque,
              DataCadastro= produtoDTOUpdateResponse.DataCadastro,
              CategoriaId= produtoDTOUpdateResponse.CategoriaId,
              Descricao= produtoDTOUpdateResponse.Descricao,
              Id= produtoDTOUpdateResponse.Id,
              ImagemUrl= produtoDTOUpdateResponse.ImagemUrl,
              Nome= produtoDTOUpdateResponse.Nome,
              Preco = produtoDTOUpdateResponse.Preco
            };
        }
        public static ProdutoDTOUpdateResponse? ToProdutoDTOUpdateResponse(this Produto produto)
        {
            if (produto is null)
                return null;
            return new ProdutoDTOUpdateResponse
            {
                Estoque = produto.Estoque,
                DataCadastro = produto.DataCadastro,
                CategoriaId = produto.CategoriaId,
                Descricao = produto.Descricao,
                Id = produto.Id,
                ImagemUrl = produto.ImagemUrl,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }
    }
}
