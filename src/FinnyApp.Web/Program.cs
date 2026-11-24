using FinnyApp.BLL.Services;
using FinnyApp.DAL.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ParentService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "FinnyUserCookie";
        options.DefaultChallengeScheme = "FinnyUserCookie";
    })
    .AddCookie("FinnyUserCookie", options =>
    {
        options.Cookie.Name = "FinnyUserCookie";
        options.LoginPath = "/Login";
    })
    .AddCookie("FinnySubUserCookie", options =>
    {
        options.Cookie.Name = "FinnySubUserCookie";
        options.LoginPath = "/SubLogin";
    });
builder.Services.AddAuthorization();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/Views/global/{0}.cshtml");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Global}/{action=Home}/{id?}")
    .WithStaticAssets();


app.Run();
