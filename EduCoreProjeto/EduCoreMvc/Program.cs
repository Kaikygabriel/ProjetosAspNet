using EduCoreMvc.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AuthProviderService>();
builder.Services.AddScoped<CourseService>();



builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("ApiEduCore", x =>
    x.BaseAddress = new Uri(builder.Configuration["ApiUri:Uri"]!));

 
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
