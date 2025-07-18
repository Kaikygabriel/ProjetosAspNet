using System.Text.Json.Serialization;
using BlibiotecaApi.Data;
using BlibiotecaApi.Extesion;
using BlibiotecaApi.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<LoggingExceptionGlobalFilter>();
builder.Services.AddControllers(options =>
    options.Filters.Add(typeof(LoggingExceptionGlobalFilter)));
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var conection = builder.Configuration.GetConnectionString("ConectionApi");
builder.Services.AddDbContext<BlibiotecaContextApi>(options =>
    options.UseMySql(
        conection,
        ServerVersion.AutoDetect(conection)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseConfigureExceptionsGlobal();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
