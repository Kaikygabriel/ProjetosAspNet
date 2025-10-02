namespace Biblioteca.Application.DTOS;

public class RegisterUserDTO
{
    public RegisterUserDTO()
    {
        
    }
    public RegisterUserDTO(string name, string emailAdress, string password)
    {
        Name = name;
        EmailAdress = emailAdress;
        Password = password;
    }

    public string Name { get; set; }
    public string EmailAdress { get; set; }
    public string Password { get; set; }
}