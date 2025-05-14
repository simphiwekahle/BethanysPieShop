namespace BethanysPieShop.Models.Repositories;

public class CategoryRepository(
    BethanysPieShopDbContext context) : ICategoryRepository
{
    public IEnumerable<Category> GetAllCategories => 
        context.Categories.OrderBy(p => p.CategoryName);
}
