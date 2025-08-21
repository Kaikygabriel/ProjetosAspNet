using System.Text;
using System.Text.Json.Serialization;
using BlibiotecaApi.Data;
using BlibiotecaApi.Extesion;
using BlibiotecaApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<LoggingExceptionGlobalFilter>();
//builder.Services.AddControllers(options =>
  //  options.Filters.Add(typeof(LoggingExceptionGlobalFilter)));
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var conection = builder.Configuration.GetConnectionString("ConectionApi");
builder.Services.AddDbContext<BlibiotecaContextApi>(options =>
    options.UseMySql(
        conection,
        ServerVersion.AutoDetect(conection)));

//identity config

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<BlibiotecaContextApi>()
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
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidateIssuer = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((builder.Configuration["Jwt:SecretKey"])))
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseConfigureExceptionsGlobal();  
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
