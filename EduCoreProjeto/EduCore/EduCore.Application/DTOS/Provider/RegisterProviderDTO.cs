namespace EduCore.Application.DTOS.Provider;

public class RegisterProviderDto
{
    public string AdressEmail { get; set; } = string.Empty;
    public string Name { get; set; }= string.Empty;
    public string Password { get; set; }= string.Empty;
}