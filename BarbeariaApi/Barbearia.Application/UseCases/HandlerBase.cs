using Barbearia.Domain.BackOffice.Interfaces;

namespace Barbearia.Application.UseCases;

public abstract class HandlerBase
{
   protected IUnitOfWork UnitOfWork;

   protected HandlerBase(IUnitOfWork unitOfWork)
   {
      this.UnitOfWork = unitOfWork;
   }
}