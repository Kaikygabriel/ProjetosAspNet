using NotifiMe.Models;

namespace NotifiMe.Extesion;

public static  class UserServicePasswordExtesion
{
    public static bool CheckPassword(this User user, string passwordPast)
    {
        if (BCrypt.Net.BCrypt.Verify(passwordPast, user.PasswordHash))
            return true;
        return false;
    }   
}