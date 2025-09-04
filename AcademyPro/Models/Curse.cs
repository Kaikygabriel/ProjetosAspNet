using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace AcademyPro.Models;

public class Curse
{
    [Key]
    [NotNull]
    public int Id { get; set; }
    [DataType(DataType.Date)]
    [NotNull]
        public DateTime DataCreated { get; set; }
    [NotNull]
    [StringLength(150,MinimumLength = 5)]
    public string? Title { get; set; }
    [StringLength(200,MinimumLength = 10)]
    [NotNull]
    public string? Description { get; set; }
    [NotNull]
    public int IdIntructor { get; set; }

    public List<User> Users { get; set; } = new List<User>();
}