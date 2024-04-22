using System.Configuration;
using SaveYourMoney_MVC.BusinessLogic;
using SaveYourMoney_MVC.BusinessLogic.Service;
using SaveYourMoney_MVC.Repositories;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// we need to add al dependencies here
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session services
builder.Services.AddSession(options =>
{
    // Set a short timeout for testing purposes
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    // Make the session cookie essential
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/LoginAndSignUp/Login"; // Customize the login path if needed
        options.AccessDeniedPath = "/Shared/AccessDenied"; // Customize the access denied path if needed
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });


builder.Services.AddScoped<ILoginManager, LoginManager>();
builder.Services.AddScoped<ISignUpManager, SignUpManager>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<IBudgetManager, BudgetManager>();
builder.Services.AddScoped<IExpenseManager, ExpenseManager>();
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginAndSignUp}/{action=Login}/{id?}");

app.Run();

