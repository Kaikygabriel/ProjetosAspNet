using System.Text;
using System.Text.Json.Serialization;
using ApiCompras;
using ApiCompras.Extesions;
using ApiCompras.Filters;
using ApiCompras.Logger;
using ApiCompras.Model;
using ApiCompras.Repository;
using ApiCompras.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<VendaContext>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); 
builder.Logging.AddProvider(new LoggingCustomProvider());
var conectionString = builder.Configuration.GetConnectionString("ConectionBanco");
builder.Services.AddDbContext<VendaContext>(options =>
    options.UseMySql(
        conectionString,
        ServerVersion.AutoDetect(conectionString)));
builder.Services.AddScoped<ServiceFiltersCustom>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IVendaRepository, VendaRepository>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            builder.Configuration["JWT:SecretKey"]
                        )),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });
builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseExceptionGlobalHandler();
}
app.UseHttpsRedirection();

app.MapControllers();
app.UseAuthorization();

app.Run();
