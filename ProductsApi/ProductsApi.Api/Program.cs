using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Infraestruct.Data.Context;
using ProductsApi.Infraestruct.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionSql = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseSqlServer(connectionSql,b => b.MigrationsAssembly("ProductsApi.Api")));
builder.Services.AddRateLimiter(x =>
    x.AddFixedWindowLimiter("Fixed", x =>
    {
        x.AutoReplenishment = true;
        x.PermitLimit = 3;
        x.QueueLimit = 1;
        x.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        x.Window = TimeSpan.FromSeconds(12);
    }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();