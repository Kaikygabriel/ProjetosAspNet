using System.Data.SqlClient;
using TodoApi2.Api.Data;

namespace TodoApi2.Api.Extesions;

public static class ServiceCollectionsExtesions
{
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddScoped<AppDbContext.GetConnection>(sp =>
            async () =>
            {
                var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            });
        return builder;
    }
}