using EduCore.Domain.Entities;
using EduCore.Domain.Exceptions;

namespace EduCore.Test.Domain.Entity;

public class ProviderTest
{
    [Fact]
    public void CreateProviderWithParametersNull_Return_ProviderException()
    {
        Assert.Throws<ProviderException>(() =>
        {
            new Provider(null, null);
        });
    }
}