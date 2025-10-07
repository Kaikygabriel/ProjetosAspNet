using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Entities;

public class ToDo
{
    public ToDo()
    {
        
    }
    public ToDo(int id, string? title, string? description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(130,MinimumLength = 4)]
    public string? Title { get; set; }

    [Required]
    [StringLength(130,MinimumLength = 4)]
    public string? Description { get; set; }
}