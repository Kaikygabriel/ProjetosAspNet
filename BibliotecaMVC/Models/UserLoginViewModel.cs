namespace BibliotecaMVC.Models;

public class UserLoginViewModel
{
    public UserLoginViewModel()
    {
        
    }
    public UserLoginViewModel(string name, string emailAdress, string password)
    {
        Name = name;
        EmailAdress = emailAdress;
        Password = password;
    }

    public string Name { get; set; }
    public string EmailAdress { get; set; }
    public string Password { get; set; }
}