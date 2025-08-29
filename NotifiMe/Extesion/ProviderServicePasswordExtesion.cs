using NotifiMe.Models;

namespace NotifiMe.Extesion;

public static class ProviderServicePasswordExtesion
{
    public static bool CheckPassword(this Provider provider, string passwordPast)
    {
        //transforma em hash 
        if (BCrypt.Net.BCrypt.Verify(passwordPast, provider.PasswordHash))
            return true;
        return false;
    }
}