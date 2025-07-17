using System.Text.Json.Serialization;
using ApiCursos.Data;
using ApiCursos.ExtesionMethods;
using ApiCursos.Filters;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ExceptionFilter>();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
builder.Services.AddControllers(options
=> options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conection = builder.Configuration.GetConnectionString("CursosConection");
builder.Services.AddDbContext<ApiCursosContext>(options =>
    options.UseMySql(
        conection,
        ServerVersion.AutoDetect(conection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionGlobalHandler();  
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
