using System.Reflection;
using EduCore.CrossCuting.ID;
using MediatorX.Core.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDependencyInjectionApplication(builder.Configuration);
builder.Services.AddMemoryCache(x =>
{
    x.SizeLimit = 300;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
