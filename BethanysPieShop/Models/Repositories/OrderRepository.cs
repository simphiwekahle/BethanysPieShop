namespace BethanysPieShop.Models.Repositories;

public class OrderRepository(
    BethanysPieShopDbContext context,
    IShoppingCart shoppingCart) : IOrderRepository
{
    public void CreateOrder(Order order)
    {
        order.OrderPlaced = DateTime.Now;

        List<ShoppingCartItem>? shoppingCartItems = shoppingCart.ShoppingCartItems;
        order.OrderTotal = shoppingCart.GetShoppingCartTotal();

        order.OrderDetails = new List<OrderDetail>();

        foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
        {
            var orderDetail = new OrderDetail()
            {
                Amount = shoppingCartItem.Amount,
                PieId = shoppingCartItem.Pie.PieId,
                Price = shoppingCartItem.Pie.Price
            };

            order.OrderDetails.Add(orderDetail);
        }
        context.Orders.Add(order);

        context.SaveChanges();
    }
}
