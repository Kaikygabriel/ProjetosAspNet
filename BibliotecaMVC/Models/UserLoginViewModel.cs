namespace BibliotecaMVC.Models;

public class UserLoginViewModel
{
    public UserLoginViewModel()
    {
        
    }
    public UserLoginViewModel(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public string Name { get; set; }
    public string Password { get; set; }
}