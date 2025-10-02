using Biblioteca.Domain.BackOffice.Entities;

namespace Biblioteca.Application.Services;

public static class UserExtesionServicePassword
{
    public static bool CheckPassword(this User user,string otherPassword)
    {
        if (BCrypt.Net.BCrypt.Verify(otherPassword, user.Password))
            return true;
        return false;
    }
}