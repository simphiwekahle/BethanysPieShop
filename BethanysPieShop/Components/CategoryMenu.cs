using BethanysPieShop.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Components;

public class CategoryMenu(
    ICategoryRepository categoryRepository) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var categories = categoryRepository.GetAllCategories.OrderBy(c => c.CategoryName);

        return View(categories);
    }
}
