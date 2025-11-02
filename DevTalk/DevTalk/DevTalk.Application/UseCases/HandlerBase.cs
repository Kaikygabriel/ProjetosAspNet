using DevTalk.Domain.BackOffice.Interfaces;

namespace DevTalk.Application.UseCases;

public abstract class HandlerBase
{
    protected IUnitOfWork UnitOfWork;

    protected HandlerBase(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}