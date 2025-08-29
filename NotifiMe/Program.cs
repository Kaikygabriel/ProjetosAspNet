using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotifiMe.Data;
using NotifiMe.Extesion;
using NotifiMe.Repository;
using NotifiMe.Repository.Interface;
using NotifiMe.Service;
using NotifiMe.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

//dependence Injection
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IUserRepository,RepositoryUser>();
builder.Services.AddScoped<IProviderRepository,RepositoryProvider>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
//connection MySql
var connection = builder.Configuration["ConnectionStrings:connection"];
builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseMySql(connection, ServerVersion.AutoDetect(connection)));
//Authentication jwt
var key = builder.Configuration["Jwt:SecretKey"];
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
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
    };
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("UserOnly", Policy => Policy.RequireRole("User"));
    x.AddPolicy("ProviderOnly",Policy =>Policy.RequireRole("Provider"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseGlobalExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
