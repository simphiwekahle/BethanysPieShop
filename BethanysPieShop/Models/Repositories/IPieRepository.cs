namespace BethanysPieShop.Models.Repositories;

public interface IPieRepository
{
    IEnumerable<Pie> GetAllPies { get;  }
    IEnumerable<Pie> PiesOfTheWeek {  get; }
    Pie? GetPieById(int pieId);
    IEnumerable<Pie> SearchPies(string searchQuery);
}
