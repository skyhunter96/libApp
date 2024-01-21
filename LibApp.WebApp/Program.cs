using Domain.Models;
using EfDataAccess;
using LibApp.Services;
using LibApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibApp;Integrated Security=True",
        b => b.MigrationsAssembly("LibApp.EfDataAccess")));

builder.Services.AddDefaultIdentity<User>(/*options => options.SignIn.RequireConfirmedAccount = true*/).AddEntityFrameworkStores<LibraryContext>();
builder.Services.AddControllersWithViews();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Enable developer exception page
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler("/Home/Error"); // Handles exceptions and redirects to the ErrorController
app.UseStatusCodePagesWithReExecute("/Error/StatusCode/{0}"); // Handles status codes and redirects to the ErrorController

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
