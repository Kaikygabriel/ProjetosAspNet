using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AcademyPro.Models;

public class Enrollment
{
    [Key]
    [NotNull]
    public int Id { get; set; }
    public int IdUser { get; set; }
    public int CourseId { get; set; }
    public DateTime DateEnrollment { get; set; }
}