using APiCursos.Data;
using ApiCursos.ExtesionMethods;
using APiCursos.Filter;
using ApiCursos.Repository;
using ApiCursos.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers(options =>
    options.Filters.Add(typeof(ExceptionGlobalFilter)));
builder.Services.AddOpenApi();
var conection = builder.Configuration.GetConnectionString("Conection");
builder.Services.AddDbContext<ApiCursoContext>(options =>
    options.UseMySql(
        conection,
        ServerVersion.AutoDetect(conection)));
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IRepositoryCurso, RepositoryCurso>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseExceptionGlobalHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
