using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.Validations
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;
            string primeiraLetra = value.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
                return new ValidationResult("Error, a primeira letra tem que ser maiuscula.");
            return ValidationResult.Success;
        } 
    }
}
