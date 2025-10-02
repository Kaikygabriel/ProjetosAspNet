namespace Biblioteca.Application.DTOS;

public class LoginUserDTO
{
    public LoginUserDTO(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public string Name { get; set; }
    public string Password { get; set; }
}