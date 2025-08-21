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
    private readonly IBlibiotecaRepository _blibiotecaRepository;

    public UnitOfWork(IBlibiotecaRepository blibiotecaRepository, BlibiotecaContextApi context)
    {
        _blibiotecaRepository = blibiotecaRepository;
        Context = context;
    }

    public IBlibiotecaRepository blibiotecaRepository
    {
        get
        {
            return _blibiotecaRepository;
        }
    }
    
}