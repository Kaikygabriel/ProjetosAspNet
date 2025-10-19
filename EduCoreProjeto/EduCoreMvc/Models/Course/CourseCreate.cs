namespace EduCoreMvc.Models.Course;

public class CourseCreate
{
    public CourseCreate()
    {
        
    }
    public CourseCreate(string title, string description, decimal price)
    {
        Title = title;
        Description = description;
        Price = price;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price{ get; set; }
    public int IdProvider { get; set; } = 1;
}