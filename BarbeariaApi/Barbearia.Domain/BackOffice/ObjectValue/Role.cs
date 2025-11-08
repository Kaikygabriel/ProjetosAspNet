using Barbearia.Domain.BackOffice.Exception;

namespace Barbearia.Domain.BackOffice.ObjectValue;

public class Role
{
    public Role(string title)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length < 2)
            throw new RoleException("Title invalid in role!");
        Title = title;
    }

    public string Title { get; set; }   
}