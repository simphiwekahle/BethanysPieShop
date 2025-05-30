﻿
namespace BethanysPieShop.Models.Repositories;

public class MockCategoryRepository : ICategoryRepository
{
    public IEnumerable<Category> GetAllCategories =>
        new List<Category>
        {
            new Category
            {
                CategoryId = 1,
                CategoryName = "Fruit pies",
                Description = "All-fruity pies"
            },
            new Category
            {
                CategoryId = 2,
                CategoryName = "Cheese Cakes",
                Description = "Cheesy all the way"
            },
            new Category
            {
                CategoryId = 3,
                CategoryName = "Seasonal pies",
                Description = "Get in the mood for a seasonal pie"
            }
        };
}
