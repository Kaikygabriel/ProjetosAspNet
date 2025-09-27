using System.Threading.RateLimiting;
using Filmes.CrossCuting.Extesions;
using Filmes.Infraestruture.Extesions;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.ExtesionsServicesInfraestructure(builder.Configuration);
builder.Services.ServiceExtesionsDbContext(builder.Configuration);
builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("limiterFixed", x =>
    {
        x.Window = TimeSpan.FromSeconds(10);
        x.PermitLimit = 3;
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
    app.UseExceptionGlobalHandler();
}
app.UseRouting();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();