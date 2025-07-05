using System.Text.Json.Serialization;
using ApiClientes.Data;
using ApiClientes.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );
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
