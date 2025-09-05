using AcademyPro.Data;
using AcademyPro.Extesion;
using AcademyPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

//Connection MySql
var connection = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

//Config Identitiy
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseExceptionGlobal();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
