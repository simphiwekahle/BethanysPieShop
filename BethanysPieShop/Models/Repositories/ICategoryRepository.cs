namespace BethanysPieShop.Models.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAllCategories {  get; }
}
