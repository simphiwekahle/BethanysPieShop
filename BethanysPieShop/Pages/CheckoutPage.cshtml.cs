using BethanysPieShop.Models;
using BethanysPieShop.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BethanysPieShop.Pages
{
    public class CheckoutPageModel(
        IOrderRepository orderRepository,
        IShoppingCart shoppingCart) : PageModel
    {
        [BindProperty]
        public Order Order { get; set; } = default!;

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some pies first");
            }

            if (ModelState.IsValid) 
            { 
                orderRepository.CreateOrder(Order);
                shoppingCart.ClearCart();
                return RedirectToPage("CheckoutCompletePage");
            }

            return Page();
        }
    }
}
