using ApiCursos.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conection = builder.Configuration.GetConnectionString("Conection");
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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
