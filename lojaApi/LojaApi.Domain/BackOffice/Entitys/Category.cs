namespace LojaApi.Domain.BackOffice.Entitys;

public class Category : Entity
{
    public Category()
    {
        
    }
    public Category(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}