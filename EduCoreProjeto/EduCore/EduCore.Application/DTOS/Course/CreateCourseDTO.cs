namespace EduCore.Application.DTOS.Course;

public class CreateCourseDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price{ get; set; }
    public int  ProviderId { get; set; }
}