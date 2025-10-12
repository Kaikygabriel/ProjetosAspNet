using System.ComponentModel.DataAnnotations;

namespace EduCoreMvc.Models.Providers;

public class RegisterProviderDto
{
    [EmailAddress]
    public string AdressEmail { get; set; } = string.Empty;
    public string Name { get; set; }= string.Empty;
    public string Password { get; set; }= string.Empty;

}