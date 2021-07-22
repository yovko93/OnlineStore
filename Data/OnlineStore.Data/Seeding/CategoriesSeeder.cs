namespace OnlineStore.Data.Seeding
{
    using System;
    using System.Collections.Generic;
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
            {
                Name = "Men's",
                Description = null,
                SubCategories = new List<SubCategory>
                {
                    new SubCategory { Name = "T-Shirts" },
                    new SubCategory { Name = "Shirts" },
                    new SubCategory { Name = "Hoodies & Sweatshirts" },
                    new SubCategory { Name = "Jackets" },
                    new SubCategory { Name = "Suits" },
                    new SubCategory { Name = "Vests" },
                    new SubCategory { Name = "Pants" },
                    new SubCategory { Name = "Shorts" },
                    new SubCategory { Name = "Jeans" },
                    new SubCategory { Name = "Underwear" },
                    new SubCategory { Name = "Hats & Caps" },
                    new SubCategory { Name = "Socks" },
                    new SubCategory { Name = "Shoes" },
                    new SubCategory { Name = "Sport Shoes" },
                },
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Women's",
                Description = null,
                SubCategories = new List<SubCategory>
                {
                    new SubCategory { Name = "Dresses" },
                    new SubCategory { Name = "Coats & Jackets" },
                    new SubCategory { Name = "Blouses & Shirts" },
                    new SubCategory { Name = "Sweaters" },
                    new SubCategory { Name = "Bikinis" },
                    new SubCategory { Name = "Skirts" },
                    new SubCategory { Name = "Pants & Jeans" },
                    new SubCategory { Name = "Belts" },
                    new SubCategory { Name = "Bags" },
                    new SubCategory { Name = "Hats" },
                    new SubCategory { Name = "Socks" },
                    new SubCategory { Name = "Heels" },
                    new SubCategory { Name = "Slippers" },
                    new SubCategory { Name = "Boots" },
                },
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Baby & Kids",
                Description = null,
                SubCategories = new List<SubCategory>
                {
                    new SubCategory { Name = "Toys" },
                    new SubCategory { Name = "Children's Books" },
                    new SubCategory { Name = "Socks" },
                    new SubCategory { Name = "Baby Shoes" },
                    new SubCategory { Name = "Baby Shirts" },
                    new SubCategory { Name = "Baby Pants" },
                    new SubCategory { Name = "Bodysuits" },
                },
            });
        }
    }
}
