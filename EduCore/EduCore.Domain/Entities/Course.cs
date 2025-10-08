namespace EduCore.Domain.Entities;

public class Course : Entity 
{
    public Course()
    {
        
    }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price{ get; set; }
    public Provider Provider { get; set; }
    public List<Student> Students { get; set; }
}