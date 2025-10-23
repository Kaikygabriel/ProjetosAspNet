using ProductsApi.Domain.BackOffice.Exceptions;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Test.Domain.BackOffice.Entity.Product;

public class ProductTest
{
    [Fact]
    public void CreateProductWithParamtersNull_Return_ProductException()
    {
        Assert.Throws<ProductException>(
            new ProductsApi.Domain.BackOffice.Entitys.Product
                (1,new Category(""),null));
    }
}