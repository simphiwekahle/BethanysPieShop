using BethanysPieShop.Models;
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

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        var items = shoppingCart.GetShoppingCartItems();
        shoppingCart.ShoppingCartItems = items;

        if (shoppingCart.ShoppingCartItems.Count == 0)
        {
            ModelState.AddModelError("", "Your cart is empty, add some pies first");
        }

        if (ModelState.IsValid)
        {
            orderRepository.CreateOrder(order);
            shoppingCart.ClearCart();

            return RedirectToAction("CheckoutComplete");
        }

        return View(order);
    }

    public IActionResult CheckoutComplete()
    {
        ViewBag.CheckoutCompleteMessage = "Thanks for the order. You'll soon be enjoying your delicious pie";

        return View();
    }
}
