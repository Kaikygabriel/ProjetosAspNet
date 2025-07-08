using BlibiotecaApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
var conection = builder.Configuration.GetConnectionString("Conection");
builder.Services.AddDbContext<BlibiotecaContext>(options =>
    options.UseMySql(
        conection,
        ServerVersion.AutoDetect(conection)
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
