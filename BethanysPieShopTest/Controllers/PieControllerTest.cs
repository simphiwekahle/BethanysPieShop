using BethanysPieShop.Controllers;
using BethanysPieShop.ViewModels;
using BethanysPieShopTest.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopTest.Controllers;

public class PieControllerTest
{
    [Fact]
    public void List_EmptyCategory_ReturnAllPies()
    {
        // Arrange
        var mockPieRepository = RepositoryMocks.GetPieRepository();
        var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

        var pieController = new PieController(mockPieRepository.Object,
            mockCategoryRepository.Object);

        // Act
        var result = pieController.List("");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(
            viewResult.ViewData.Model);

        Assert.Equal(10, pieListViewModel.Pies.Count());
    }
}
