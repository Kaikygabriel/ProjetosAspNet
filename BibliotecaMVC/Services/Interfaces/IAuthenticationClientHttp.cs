using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services.Interfaces;

public interface IAuthenticationClientHttp
{
    Task<bool> AuthenticationRegisterAsync(UserRegisterViewModel model);
    Task<TokenViewModel> AuthenticationLoginAsync(UserLoginViewModel model);
}