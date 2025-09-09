using System.Text;
using System.Threading.RateLimiting;
using AcademyPro.Data;
using AcademyPro.Extesion;
using AcademyPro.Models;
using AcademyPro.Repository;
using AcademyPro.Repository.Interfaces;
using JwtCraft;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

JwtOptions.SecretKey = "teste@Adfdfaksdfjaksjfdlja11afda";
JwtOptions.Audience = "https://localhost:7056";
JwtOptions.Issuer = "https://localhost:5050";
JwtOptions.TokenValidInMinutes = 60;

//Injeção de dependencia
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUnitOfWOrk,UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICurseRepository,CurseRepository>();
builder.Services.AddScoped<IEnrollmentRepository,EnrollmentRepository>();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddScoped<ITokenService,JwtService>();

//Connection MySql
var connection = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

//Config Identitiy
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//Authentication/Authorization Jwt
// builder.Services.AddAuthentication(x =>
// {
//     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(options =>
// {
//     options.SaveToken = true;
//     options.RequireHttpsMetadata = false;
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ClockSkew = TimeSpan.Zero,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         ValidAudience = builder.Configuration["Jwt:Audience"]
//     };
// });

//RateLimiter

builder.Services.AddRateLimiter(options =>
    options.AddFixedWindowLimiter("FixedRate",x =>
    {
        x.Window = TimeSpan.FromSeconds(10);
        x.PermitLimit = 3;
        x.QueueLimit = 1;
        x.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
    app.UseExceptionGlobal();
}

app.UseRouting();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
