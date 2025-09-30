namespace Filmes.Domain.Exceptions;

public class EmailException(string menssage) : ApplicationException(menssage);