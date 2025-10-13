using MediatorX.Core.Abstraction.Interfaces;

namespace LojaApi.Application.UseCases.Product.Commands.Delete;

public class DeleteProductRequest : IRequest<bool>
{
    public DeleteProductRequest()
    {
        
    }
    public DeleteProductRequest(Domain.BackOffice.Entitys.Product product)
    {
        Product = product;
    }

    public Domain.BackOffice.Entitys.Product Product { get; set; }   
}