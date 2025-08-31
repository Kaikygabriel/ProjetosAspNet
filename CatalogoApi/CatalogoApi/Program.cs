using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using CatalogoApi.AutoMapper;
using CatalogoApi.Data;
using CatalogoApi.Extesions;
using CatalogoApi.Filters;
using CatalogoApi.Logging;
using CatalogoApi.Model;
using CatalogoApi.Repository;
using CatalogoApi.Repository.Interface;
using CatalogoApi.Services;
using CatalogoApi.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CatalogoContext>()
    .AddDefaultTokenProviders();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel= LogLevel.Information
}));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepositoryProduto, RepositoryProduto>(); 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepositoryCategoria, RepositoryCategoria>();

//builder.Services.AddControllers(options =>
//options.Filters.Add(typeof(ApiExceptionFilter))
//);

builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options => { 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Apicatalogo",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer   jwt"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddScoped<SeedingService>(); 
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddDbContext<CatalogoContext>(Options =>
    Options.UseMySql(
        builder.Configuration.GetConnectionString("Catalogo"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Catalogo")
)));
var secretKey = builder.Configuration["JWT:SecretKey"]?? throw new Exception("chave null");
builder.Services.AddAuthentication (options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
} ).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Adimin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));

    options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("Adimin")
        .RequireClaim("id", "kaiky"));

    options.AddPolicy("ExclusiveOnly", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim =>
            claim.Type == "id" &&
            claim.Value == "kaiky" ||
            context.User.IsInRole("SuperAdmin"))));
});
builder.Services.AddAutoMapper(typeof(DomationToProfileMapper));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHanler(); 
}
using (var scoped = app.Services.CreateScope())
{
    var service = scoped.ServiceProvider;
    var seeding = service.GetRequiredService<SeedingService>();
    seeding.Seed();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();
