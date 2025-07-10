using System.ComponentModel.DataAnnotations;
using ApiClientes.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiClientes.Validations;

public class NameInUseAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var context = (ClienteContext)validationContext.GetService(typeof(ClienteContext))!;
        string name = value.ToString();
        if (context.Clientes.Any(x => x.Name == name))
            return new ValidationResult("Error, esté nome ja esta em uso.");
        return ValidationResult.Success;
    }
}