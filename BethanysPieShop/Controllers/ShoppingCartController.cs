using BethanysPieShop.Models.Repositories;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers;

public class ShoppingCartController(
    IPieRepository pieRepository,
    IShoppingCart shoppingCart) : Controller
{
    public IActionResult Index()
    {
        var items = shoppingCart.GetShoppingCartItems();
        shoppingCart.ShoppingCartItems = items;

        var shoppingCartViewModel = new ShoppingCartViewModel(
            shoppingCart,
            shoppingCart.GetShoppingCartTotal());

        return View(shoppingCartViewModel);
    }

    public RedirectToActionResult AddToShoppingCart(int pieId)
    {
        var selectedPie = pieRepository.GetAllPies.FirstOrDefault(p => p.PieId == pieId);

        if (selectedPie is not null)
        {
            shoppingCart.AddToCart(selectedPie);
        }
        return RedirectToAction("Index");
    }
    
    public RedirectToActionResult RemoveFromShoppingCart(int pieId)
    {
        var selectedPie = pieRepository.GetAllPies.FirstOrDefault(p => p.PieId == pieId);

        if (selectedPie is not null)
        {
            shoppingCart.RemoveFromCart(selectedPie);
        }
        return RedirectToAction("Index");
    }
}
