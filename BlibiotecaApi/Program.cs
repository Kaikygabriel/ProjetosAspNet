using System.Text.Json.Serialization;
using BlibiotecaApi.Data;
using BlibiotecaApi.Extesion;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var conection = builder.Configuration.GetConnectionString("ConectionApi");
builder.Services.AddDbContext<BlibiotecaContextApi>(options =>
    options.UseMySql(
        conection,
        ServerVersion.AutoDetect(conection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseConfigureExceptionsGlobal();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
