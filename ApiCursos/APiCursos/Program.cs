using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using APiCursos.Data;
using ApiCursos.ExtesionMethods;
using APiCursos.Filter;
using APiCursos.Model.DTO;
using ApiCursos.Repository;
using ApiCursos.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ApiCursos.Service;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddJsonOptions(options=>
    options.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles);
builder.Services.AddOpenApi();
var conection = builder.Configuration.GetConnectionString("Conection");
builder.Services.AddDbContext<ApiCursoContext>(options =>
    options.UseMySql(
        conection,
        ServerVersion.AutoDetect(conection)));
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IRepositoryCurso, RepositoryCurso>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<RepositoryUsers>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApiCursoContext>()
    .AddDefaultTokenProviders();
var key = builder.Configuration["JWT:SecretKey"];
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("Fixed", x =>
    {
        x.PermitLimit = 3;
        x.QueueLimit = 1;
        x.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        x.Window = TimeSpan.FromSeconds(10);
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseExceptionGlobalHandler();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
