using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using NotifiMe.Atributte;

namespace NotifiMe.Models.DTO;

public class RequestCreateAppointmentDTO
{
    public  string  userName{ get; set; }= string.Empty;
    public string UserPassword { get; set; }= string.Empty;
    public string ProviderName { get; set; } = string.Empty;
    [DataType(DataType.DateTime)] 
    [DateAppoimentCheck]
    public DateTime DateAppointment { get; set; } 
}