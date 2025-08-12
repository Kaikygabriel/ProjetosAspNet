using System.Text;
using System.Text.Json.Serialization;
using ApiClientes.Data;
using ApiClientes.Extesion;
using ApiClientes.Filters;
using ApiClientes.Repository;
using ApiClientes.Repository.Interfaces;
using ApiClientes.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ClienteContext>();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );
builder.Services.AddScoped<ExceptionFilterGlobal>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddControllers(options =>
    options.Filters.Add(typeof(ExceptionFilterGlobal))
);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IClientesRepository, ClienteRepository>();
builder.Services.AddScoped<SeedingCliente>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conectionString = builder.Configuration.GetConnectionString("ConenctionClient");
builder.Services.AddDbContext<ClienteContext>(optons =>
    optons.UseMySql(
        conectionString,
        ServerVersion.AutoDetect(conectionString)));
var key = builder.Configuration["JWT:SecretKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});
builder.Services.AddAuthorization();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionGlobalHandler();
}

using (var scoped = app.Services.CreateScope())
{
    var service = scoped.ServiceProvider;
    var seding = service.GetRequiredService<SeedingCliente>();
    seding.Seed();
}
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
