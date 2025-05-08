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
            "Cheese cakes");

        return View(pieListViewModel);
    }
}
