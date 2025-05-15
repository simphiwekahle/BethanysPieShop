using BethanysPieShop.Models.Repositories;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Components;

public class ShoppingCartSummary(
    IShoppingCart shoppingCart) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var items = shoppingCart.GetShoppingCartItems();
        shoppingCart.ShoppingCartItems = items;

        var shoppingCartViewModel = new ShoppingCartViewModel(
            shoppingCart,
            shoppingCart.GetShoppingCartTotal());

        return View(shoppingCartViewModel);
    }
}
