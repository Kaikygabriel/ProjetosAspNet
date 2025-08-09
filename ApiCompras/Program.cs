using System.Text.Json.Serialization;
using ApiCompras;
using ApiCompras.Extesions;
using ApiCompras.Filters;
using ApiCompras.Logger;
using ApiCompras.Model;
using ApiCompras.Repository;
using ApiCompras.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); 
builder.Logging.AddProvider(new LoggingCustomProvider());
var conectionString = builder.Configuration.GetConnectionString("ConectionBanco");
builder.Services.AddDbContext<VendaContext>(options =>
    options.UseMySql(
        conectionString,
        ServerVersion.AutoDetect(conectionString)));
builder.Services.AddScoped<ServiceFiltersCustom>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IVendaRepository, VendaRepository>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddBearerToken();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseExceptionGlobalHandler();
}
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
