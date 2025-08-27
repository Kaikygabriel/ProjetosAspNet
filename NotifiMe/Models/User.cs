using System.ComponentModel.DataAnnotations;

namespace NotifiMe.Models;

public class User
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(160,MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public ICollection<Appointment>Appointments { get; set; }
}