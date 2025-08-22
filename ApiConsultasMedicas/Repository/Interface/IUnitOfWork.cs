namespace ApiConsultasMedicas.Repository.Interface;


public interface IUnitOfWork
{
    Task Commit();
    public IConsultaRepository consultaRepository { get; }
}