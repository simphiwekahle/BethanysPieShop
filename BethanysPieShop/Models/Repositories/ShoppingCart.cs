using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models.Repositories;

public class ShoppingCart(
    BethanysPieShopDbContext dbContext) : IShoppingCart
{
    public string? ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

    public static ShoppingCart GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()
            ?.HttpContext?.Session;

        BethanysPieShopDbContext context = services.GetService<BethanysPieShopDbContext>()
            ?? throw new Exception("Error Initialising");

        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

        session?.SetString("CartId", cartId);

        return new ShoppingCart(context) { ShoppingCartId = cartId };
    }

    public void AddToCart(Pie pie)
    {
        var shoppingCartItem = dbContext.ShoppingCartItems.SingleOrDefault(
            s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Pie = pie,
                Amount = 1
            };

            dbContext.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }
        dbContext.SaveChanges();
    }

    public void ClearCart()
    {
        var cartItems = dbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

        dbContext.ShoppingCartItems.RemoveRange(cartItems);

        dbContext.SaveChanges();
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??= 
            dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Pie).ToList();
    }

    public decimal GetShoppingCartTotal()
    {
        var total = dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(c => c.Pie.Price * c.Amount).Sum();

        return total;
    }

    public int RemoveFromCart(Pie pie)
    {
        var shoppingCartItem = dbContext.ShoppingCartItems.SingleOrDefault(
            s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

        var localAmount = 0;

        if (shoppingCartItem is not null)
        {
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
                localAmount = shoppingCartItem.Amount;
            }
            else
            {
                dbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }
        dbContext.SaveChanges();

        return localAmount;
    }
}
