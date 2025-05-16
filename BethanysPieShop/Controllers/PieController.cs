using BethanysPieShop.Models;
using BethanysPieShop.Models.Repositories;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers;

public class PieController(
    IPieRepository pieRepository,
    ICategoryRepository categoryRepository) : Controller
{
    public IActionResult List(string category)
    {
        IEnumerable<Pie> pies;
        string? currentCategory;

        if (string.IsNullOrEmpty(category))
        {
            PieListViewModel pieListViewModel = new PieListViewModel(
                pieRepository.GetAllPies,
                "All Pies");

            return View(pieListViewModel);
        }
        else
        {
            pies = pieRepository.GetAllPies.Where(p => p.Category.CategoryName == category)
                .OrderBy(p => p.PieId);
            currentCategory = categoryRepository.GetAllCategories
                .FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
        }
        return View(new PieListViewModel (pies, currentCategory));
        
    }

    public IActionResult Details(int id)
    {
        var pie = pieRepository.GetPieById(id);

        if (pie == null)
            return NotFound();
        
        return View(pie);
    }
}
