using ApiCompras;
using ApiCompras.Extesions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var conectionString = builder.Configuration.GetConnectionString("ConectionBanco");
builder.Services.AddDbContext<VendaContext>(options =>
    options.UseMySql(
        conectionString,
        ServerVersion.AutoDetect(conectionString)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseExceptionGlobalHandler();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
