using FilmesApi.AutoMapper;
using FilmesApi.Data;
using FilmesApi.Extesion;
using FilmesApi.Repository;
using FilmesApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
//builder 
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddOpenApi();

var connectionMySql = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionMySql,ServerVersion.AutoDetect(connectionMySql)));
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddAutoMapper(typeof(DomainToProfile));
//pipeline
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
