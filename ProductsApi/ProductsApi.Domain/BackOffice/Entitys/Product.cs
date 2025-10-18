using System.ComponentModel.DataAnnotations;
using ProductsApi.Domain.BackOffice.Exceptions;
using ProductsApi.Domain.BackOffice.Interfaces;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Domain.BackOffice.Entitys;

public class Product  : Entity,IAggregateRoot 
{
    protected Product() { }
    public Product(decimal price, Category category, string name)
    {
        if (string.IsNullOrEmpty(name) || price <= 0)
            throw new ProductException("Error in constructor from product");
        Price = price;
        Category = category;
        Name = name;
    }

    public decimal Price { get;private set; }
    public Category Category  { get;private set; }
    [MinLength(3)]
    public string Name { get; private set; }

    public void SetPrice(decimal value)
        => Price = value;
}