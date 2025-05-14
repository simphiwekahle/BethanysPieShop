using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models.Repositories;

public class PieRepository(
    BethanysPieShopDbContext context) : IPieRepository
{
    public IEnumerable<Pie> GetAllPies 
    {
        get
        {
            return context.Pies.Include(c => c.Category);
        }
    }

    public IEnumerable<Pie> PiesOfTheWeek
    {
        get
        {
            return context.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
        }
    }

    public Pie? GetPieById(int pieId)
    {
        return context.Pies.FirstOrDefault(p => p.PieId == pieId);
    }
}
