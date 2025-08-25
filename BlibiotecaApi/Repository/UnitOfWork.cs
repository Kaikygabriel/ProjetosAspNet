using BlibiotecaApi.Data;
using BlibiotecaApi.Repository.Interfaces;

namespace BlibiotecaApi.Repository;

public class UnitOfWork : IUnitOfWork
{

    public async Task Commit()
    {
        await Context.SaveChangesAsync();
    }
    public BlibiotecaContextApi Context;
    private IBlibiotecaRepository _blibiotecaRepository;

    public UnitOfWork(BlibiotecaContextApi context)
    { Context = context;
    }

    public IBlibiotecaRepository blibiotecaRepository
    {
        get
        {
            return _blibiotecaRepository = _blibiotecaRepository ?? new BlibiotecaRepository(Context);
        }
    }
    
}