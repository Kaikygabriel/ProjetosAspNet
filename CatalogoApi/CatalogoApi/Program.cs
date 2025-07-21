using System.Text.Json.Serialization;
using CatalogoApi.Data;
using CatalogoApi.Extesions;
using CatalogoApi.Filters;
using CatalogoApi.Logging;
using CatalogoApi.Repository;
using CatalogoApi.Repository.Interface;
using CatalogoApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel= LogLevel.Information
}));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepositoryProduto, RepositoryProduto>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepositoryCategoria, RepositoryCategoria>();
builder.Services.AddControllers(options =>
options.Filters.Add(typeof(ApiExceptionFilter))
);
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
