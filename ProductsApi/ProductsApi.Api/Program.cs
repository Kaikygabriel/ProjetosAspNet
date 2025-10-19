using Microsoft.EntityFrameworkCore;
using ProductsApi.Infraestruct.Data.Context;
using ProductsApi.Infraestruct.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionSql = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseSqlServer(connectionSql,b => b.MigrationsAssembly("ProductsApi.Api")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();