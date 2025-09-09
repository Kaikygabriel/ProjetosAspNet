using System.ComponentModel.DataAnnotations;

namespace NotifiMe.Models;

public class User
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(160, MinimumLength = 3)]
    public string Name { get; set; }= string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; }= string.Empty;
    [Required]
    public string? PasswordHash { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }

    public List<Appointment> Appointments { get; set; } = new();

    public bool CheckValidateDate(DateTime dateHoursAppointmentsPast)
    {
        foreach (var ap in Appointments)
            if (ap.DateAppointment.Hour == dateHoursAppointmentsPast.Hour)
                return false;
        return true;
    }
}