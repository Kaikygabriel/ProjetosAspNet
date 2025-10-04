using Biblioteca.Domain.BackOffice.Exceptions;

namespace Biblioteca.Domain.BackOffice.Entities;

public class Author : Entity
{
    public Author()
    {
        
    }
    public Author(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new AuthorException("Name in author is null or empty ");
        Name = name;
    }
    public string Name { get; set; }
}