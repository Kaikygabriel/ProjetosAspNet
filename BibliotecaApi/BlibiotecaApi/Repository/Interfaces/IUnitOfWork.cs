namespace BlibiotecaApi.Repository.Interfaces;

public interface IUnitOfWork
{
    Task Commit();
    IBlibiotecaRepository blibiotecaRepository{ get; }
}