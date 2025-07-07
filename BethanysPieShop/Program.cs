using BethanysPieShop.App;
using BethanysPieShop.Models;
using BethanysPieShop.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(
    sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews() // Ensures that our app knows about ASP.NET Core MVC
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }); 

builder.Services.AddRazorPages();

builder.Services.AddRazorComponents()
                    .AddInteractiveServerComponents();

builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
});

//builder.Services.AddControllers(); // Required for APIs

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

app.UseAntiforgery();

app.MapRazorPages();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//app.MapControllers(); // Required for APIs

DbInitializer.Seed(app);

app.Run();
