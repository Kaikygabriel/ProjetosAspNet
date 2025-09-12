using System.Text;
using System.Threading.RateLimiting;
using FilmesApi.Data;
using FilmesApi.Extesion;
using FilmesApi.Models;
using FilmesApi.Repository;
using FilmesApi.Repository.Interfaces;
using FilmesApi.Service;
using FilmesApi.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

//builder 
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

var connectionMySql = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionMySql,ServerVersion.AutoDetect(connectionMySql)));

builder.Services.AddIdentity<User,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.Zero,
        ValidateAudience = true,
        ValidateIssuer = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});

builder.Services.AddRateLimiter(x =>
    x.AddFixedWindowLimiter("FixedLimiting", x =>
    {
        x.Window = TimeSpan.FromSeconds(10);
        x.PermitLimit = 3;
        x.QueueLimit = 1;
        x.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        x.AutoReplenishment = true;
    }));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseExceptionGlobalHandler();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
