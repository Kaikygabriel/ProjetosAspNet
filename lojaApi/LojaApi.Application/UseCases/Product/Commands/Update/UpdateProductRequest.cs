using MediatorX.Core.Abstraction.Interfaces;

namespace LojaApi.Application.UseCases.Product.Commands.Update;

public class UpdateProductRequest : IRequest<bool>
{
    public UpdateProductRequest()
    {
        
    }
    public UpdateProductRequest(Domain.BackOffice.Entitys.Product product)
    {
        Product = product;
    }

    public Domain.BackOffice.Entitys.Product Product { get; set; }   
}