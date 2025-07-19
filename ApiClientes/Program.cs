using System.Text.Json.Serialization;
using ApiClientes.Data;
using ApiClientes.Extesion;
using ApiClientes.Repository;
using ApiClientes.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );

builder.Services.AddScoped<IClientesRepository, ClienteRepository>();
builder.Services.AddScoped<SeedingCliente>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conectionString = builder.Configuration.GetConnectionString("ConenctionClient");
builder.Services.AddDbContext<ClienteContext>(optons =>
    optons.UseMySql(
        conectionString,
        ServerVersion.AutoDetect(conectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionGlobalHandler();
}

using (var scoped = app.Services.CreateScope())
{
    var service = scoped.ServiceProvider;
    var seding = service.GetRequiredService<SeedingCliente>();
    seding.Seed();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
