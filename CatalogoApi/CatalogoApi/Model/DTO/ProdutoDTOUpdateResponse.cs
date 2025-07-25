using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CatalogoApi.Validations;

namespace CatalogoApi.Model.DTO
{
    public class ProdutoDTOUpdateResponse
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
  
        public Decimal Preco { get; set; }

        public string? ImagemUrl { get; set; }

        public int Estoque { get; set; }

        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
    }
}
