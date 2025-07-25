using System.ComponentModel.DataAnnotations;
using CatalogoApi.Validations;

namespace CatalogoApi.Model.DTO
{
    public class ProdutoDTOUpdateRequest
    {

        [Range(0, 10000)]
        public int Estoque { get; set; }

        [Display(Name = "Data Cadastro")]
        [DataType(DataType.DateTime)]
        [ProdutoDTODateTimeValidation]
        public DateTime DataCadastro { get; set; }
    }
}
