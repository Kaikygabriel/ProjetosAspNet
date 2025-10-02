using System.Threading.RateLimiting;
using Biblioteca.CrosCuting.InjectionDependency;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddServicesInjectionDependency(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache(x =>
    x.SizeLimit = 1024);
builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("Fixed", x =>
    {
        x.PermitLimit = 3;
        x.Window = TimeSpan.FromSeconds(12);
        x.QueueLimit = 1;
        x.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        x.AutoReplenishment = true;
    });
    x.RejectionStatusCode = 429;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
