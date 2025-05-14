using BethanysPieShop.Models.Repositories;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers;

public class HomeController(
    IPieRepository pieRepository) : Controller
{
    public IActionResult Index()
    {
        var piesOfTheWeek = pieRepository.PiesOfTheWeek;
        var homeViewModel = new HomeViewModel(piesOfTheWeek);

        return View(homeViewModel);
    }
}
