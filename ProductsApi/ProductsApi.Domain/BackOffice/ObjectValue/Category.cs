using ProductsApi.Domain.BackOffice.Exceptions;

namespace ProductsApi.Domain.BackOffice.ObjectValue;

public class Category 
{
    protected Category(){}
    public Category(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new CategoryException("Name from category is null or empty");
        Name = name;
    }
    public string  Name { get;private set; }

}