using ApiConsultasMedicas.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Conection = builder.Configuration.GetConnectionString("ContextConection");



builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApiConsultaContext>(options =>
    options.UseMySql(
        Conection,
        ServerVersion.AutoDetect(Conection)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
