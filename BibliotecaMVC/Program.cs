using System.Net.Http.Headers;
using BibliotecaMVC.Services;
using BibliotecaMVC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("BookClient",x =>
    x.BaseAddress = new Uri(builder.Configuration["Uri:Adress"]!));
builder.Services.AddHttpClient("Authentication", x =>
{
    x.BaseAddress = new Uri(builder.Configuration["Uri:Adress"]!);
    x.DefaultRequestHeaders.Accept.Clear();
    x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<IServiceClientHttpBook, ServiceClientHttpBook>();
builder.Services.AddScoped<IAuthenticationClientHttp,AuthenticaionClientHttp>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
