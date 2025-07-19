using ApiClientes.Model;

namespace ApiClientes.Repository;

public interface IClientesRepository
{
    IEnumerable<Cliente> GetClientes();
    Cliente GetCliente(int id);
    Cliente Create(Cliente cliente);
    Cliente Update(Cliente cliente);
    Cliente Delete(int id);
}