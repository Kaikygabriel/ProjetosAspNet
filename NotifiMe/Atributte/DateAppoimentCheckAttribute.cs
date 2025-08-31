using System.ComponentModel.DataAnnotations;

namespace NotifiMe.Atributte;

public class DateAppoimentCheckAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("date is null");
        var date = (DateTime)value!;
        if(date <= DateTime.Now)
            return new ValidationResult("the date is less than or equal to the current date");
        return ValidationResult.Success;
    }
}