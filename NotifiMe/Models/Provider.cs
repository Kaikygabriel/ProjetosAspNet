using System.ComponentModel.DataAnnotations;

namespace NotifiMe.Models;

public class Provider
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(160,MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required] 
    public string PasswordHash { get; set; }
    [Required]
    public string Work { get; set; }
    
    public ICollection<Appointment>Appointments { get; set; }
}