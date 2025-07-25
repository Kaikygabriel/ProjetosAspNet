using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.Validations
{
    public class ProdutoDTODateTimeValidationAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime date = (DateTime)value ;
            if(date.Date <= DateTime.Now.Date)
                return new ValidationResult("A data deve ser maior que a data atual");
            return ValidationResult.Success;
        }
    }
}
