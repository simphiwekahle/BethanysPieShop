using BethanysPieShop.Models;
using BethanysPieShop.Models.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(
    sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews(); // Ensures that our app knows about ASP.NET Core MVC

builder.Services.AddRazorPages();

builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
});

var app = builder.Build();

app.UseStaticFiles(); // Pre-configured to look for incoming requests for static files
/*
    In .NET 9, we use the new middleware below for static files related requests
    'app.MapStaticAssets();'
*/

app.UseSession();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // This allows to see errors from an executed application
}

app.MapDefaultControllerRoute();
// Deafult Controller Route
//
// name: "default"
// pattern: "{controller=Home}/{action=Index}/{id:int?}"

/* 
    * This enables the ability to navigate to our pages.
    * Ensures that ASP.NET Core is able to handle incoming requests.
    * This is an endpoint middleware
*/

app.MapRazorPages();

DbInitializer.Seed(app);

app.Run();
