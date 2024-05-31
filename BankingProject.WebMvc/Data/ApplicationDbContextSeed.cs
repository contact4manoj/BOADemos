using Microsoft.EntityFrameworkCore;

namespace BankingProject.WebMvc.Data;

internal static class ApplicationDbContextSeed
{

    internal static async Task SeedCategoriesAsync(this ApplicationDbContext dbContext)
    {
        Category[] categories =
        {
            new Category { CategoryName = "Vegetables" },
            new() { CategoryName = "Fruits" }
        };

        if(dbContext is not null && dbContext.Categories is not null)
        {
            foreach(Category category in categories)
            {
                bool exists 
                    = await dbContext.Categories.AnyAsync(c => c.CategoryName == category.CategoryName);
                if (!exists)
                {
                    dbContext.Categories.Add(category);
                }
            }

            // Push the changes from the DbContext to the database.
            await dbContext.SaveChangesAsync();
        }
    }
}
