using BethanysPieShop.Models.Repositories;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers;

public class PieController(
    IPieRepository pieRepository,
    ICategoryRepository categoryRepository) : Controller
{
    public IActionResult List()
    {
        PieListViewModel pieListViewModel = new PieListViewModel(
            pieRepository.GetAllPies,
            "All Pies");

        return View(pieListViewModel);
    }

    public IActionResult Details(int id)
    {
        var pie = pieRepository.GetPieById(id);

        if (pie == null)
            return NotFound();
        
        return View(pie);
    }
}
