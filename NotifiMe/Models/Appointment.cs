namespace NotifiMe.Models;

public class Appointment
{
    public int Id { get; set; }
    
    public  User user{ get; set; }
    public Provider Provider{ get; set; }

    public DateTime DateAppointment{ get; set; }
    public bool IsCanceled { get; set; }
    public DateTime DateFromCreated { get; set; }
}