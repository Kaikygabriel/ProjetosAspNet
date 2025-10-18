using ProductsApi.Domain.BackOffice.Interfaces;

namespace ProductsApi.Application.UseCases;

public abstract class HandlerBase
{
    protected readonly IUnitOfWork UnitOfWork; 
    protected HandlerBase(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}