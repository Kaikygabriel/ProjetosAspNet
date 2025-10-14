using System.Data;

namespace TodoApi2.Api.Data;

public class AppDbContext
{
    public delegate Task<IDbConnection> GetConnection();
}