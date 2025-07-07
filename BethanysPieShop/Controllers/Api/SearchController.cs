using BethanysPieShop.Models;
using BethanysPieShop.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Api;

[Route("api/[controller]")] // Attribute-based routing implementation
[ApiController]
public class SearchController(
    IPieRepository pieRepository) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var allPies = pieRepository.GetAllPies;

        return Ok(allPies);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (!pieRepository.GetAllPies.Any(p => p.PieId == id))
            return NotFound();

        return Ok(pieRepository.GetAllPies.Where(p => p.PieId == id));
    }

    [HttpPost]
    public IActionResult SearchPies([FromBody] string searchQuery)
    {
        IEnumerable<Pie> pies = [];

        if (!string.IsNullOrEmpty(searchQuery))
            pies = pieRepository.SearchPies(searchQuery);

        return new JsonResult(pies);
    }
}
