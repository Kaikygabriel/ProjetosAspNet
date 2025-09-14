using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using NotifiMe.Atributte;
using Swashbuckle.AspNetCore.SwaggerGen;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace NotifiMe.Models;

public class Appointment
{
    [Required]
    [Key]
    public int Id { get; set; }
    public  int IdUser{ get; set; }

    public int IdProvider { get; set; }
    [DataType(DataType.DateTime)] 
    [DateAppoimentCheck]
    public DateTime DateAppointment { get; set; } 
    public bool IsCanceled { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime DateFromCreated { get; set; }= DateTime.Now;
}