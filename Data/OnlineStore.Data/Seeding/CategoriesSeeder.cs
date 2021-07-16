namespace OnlineStore.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineStore.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category
            { Name = "Clothing", Description = null });

            await dbContext.Categories.AddAsync(new Category
            { Name = "Shoes", Description = null });

            await dbContext.Categories.AddAsync(new Category
            { Name = "Books, movies & music", Description = null });

            await dbContext.Categories.AddAsync(new Category
            { Name = "Games", Description = null });

            await dbContext.Categories.AddAsync(new Category
            { Name = "Cosmetics & body care", Description = null });

            await dbContext.Categories.AddAsync(new Category
            { Name = "Food & drinks", Description = null });

            await dbContext.Categories.AddAsync(new Category
            { Name = "Furniture", Description = null });

            await dbContext.Categories.AddAsync(new Category
            { Name = "Sports & outdoor", Description = null });

            await dbContext.Categories.AddAsync(new Category
            { Name = "Toys & baby products", Description = null });

            await dbContext.Categories.AddAsync(new Category
            { Name = "Garden", Description = null });
        }
    }
}
