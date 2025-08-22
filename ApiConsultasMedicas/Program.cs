using System.Text;
using ApiConsultasMedicas.Data;
using ApiConsultasMedicas.Extesion;
using ApiConsultasMedicas.Models;
using ApiConsultasMedicas.Repository;
using ApiConsultasMedicas.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var Conection = builder.Configuration.GetConnectionString("ContextConection");
builder.Services.AddControllers();
builder.Services.AddOpenApi();
 
builder.Services.AddDbContext<ApiConsultaContext>(options =>
    options.UseMySql(
        Conection,
        ServerVersion.AutoDetect(Conection)));

builder.Services.AddIdentity<User, IdentityRole>()
.AddEntityFrameworkStores<ApiConsultaContext>();

var key = builder.Configuration["Jwt:SecretKey"] ?? throw new Exception();
builder.Services.AddAuthentication(x =>
{
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();,,



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   // app.UseSwaggerUI();
  //  app.UseSwagger();
    app.MapOpenApi();
    app.UseExceptioNGlobalHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
