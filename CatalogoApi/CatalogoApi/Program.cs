using System.Text.Json.Serialization;
using CatalogoApi.Data;
using CatalogoApi.Extesions;
using CatalogoApi.Filters;
using CatalogoApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options => { 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SeedingService>();
builder.Services.AddDbContext<CatalogoContext>(Options =>
    Options.UseMySql(
        builder.Configuration.GetConnectionString("Catalogo"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Catalogo")
)));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHanler(); 
}
using (var scoped = app.Services.CreateScope())
{
    var service = scoped.ServiceProvider;
    var seeding = service.GetRequiredService<SeedingService>();
    seeding.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
