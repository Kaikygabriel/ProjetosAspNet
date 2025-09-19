using Filmes.Domain.Entities;

namespace Filmes.Application.Services;

public static class ExtesionVerifyPasswordUserService
{
    public static bool CheckPassword(this User user,string otherPassword)
    {
        if (BCrypt.Net.BCrypt.Verify(otherPassword, user.PasswordHash))
            return true;
        return false;
    }
}