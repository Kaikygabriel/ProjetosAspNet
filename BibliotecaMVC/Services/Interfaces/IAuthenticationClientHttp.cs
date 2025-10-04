using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services.Interfaces;

public interface IAuthenticationClientHttp
{
    Task<bool> AuthenticationAsync(UserLoginViewModel model);
}