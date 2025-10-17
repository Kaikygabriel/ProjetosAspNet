using System.ComponentModel.DataAnnotations;

namespace EduCoreMvc.Models.Providers;

public class LoginProviderDto
{
    [StringLength(100,MinimumLength = 3)]
    [Required]
    public string Name { get; set; } = string.Empty;
    [StringLength(70,MinimumLength = 4)]
    [Required]
    public string Password { get; set; } = string.Empty;
}