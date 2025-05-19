using BethanysPieShop.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers;

public class OrderController(
    IOrderRepository orderRepository,
    IShoppingCart shoppingCart) : Controller
{
    public IActionResult Checkout()
    {
        return View();
    }
}
