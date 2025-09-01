using System.ComponentModel.DataAnnotations;

namespace NotifiMe.Models;

public class Provider
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(160, MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public string Work { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }

    public List<Appointment> Appointments { get; set; } = new();


    public bool CheckValidateDate(DateTime dateAndHours)
    {
        foreach (var ap in Appointments)
            if (ap.DateAppointment.Hour == dateAndHours.Hour) 
                return false;
        return true;
    }
}