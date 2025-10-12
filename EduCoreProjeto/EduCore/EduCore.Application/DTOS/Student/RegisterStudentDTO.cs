namespace EduCore.Application.DTOS.Student;

public class RegisterStudentDTO
{
    public string AdressEmail { get; set; } = string.Empty;
    public string Name { get; set; }= string.Empty;
    public string Password { get; set; }= string.Empty;
}