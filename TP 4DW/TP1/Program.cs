using Microsoft.EntityFrameworkCore;
using TP1.Data;
using TP1.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuration de la base de données
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=app.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("MaBasededonéées"));

// 2. Services MVC de base
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. Pipeline de requêtes
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();