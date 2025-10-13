using MediatorX.Core.Abstraction.Interfaces;

namespace LojaApi.Application.UseCases.Product.Commands.Create;

public class CreateProductRequest : IRequest<bool>
{
    public CreateProductRequest()
    {
        
    }
    public CreateProductRequest(Domain.BackOffice.Entitys.Product product)
    {
        Product = product;
    }

    public Domain.BackOffice.Entitys.Product Product { get; set; }   
}