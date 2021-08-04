namespace OnlineStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using OnlineStore.Data.Common.Repositories;
    using OnlineStore.Data.Models;
    using OnlineStore.Web.ViewModels.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryServiceModel> GetByIdAsync(int id)
        {
            if (!await this.ContainsByIdAsync(id))
            {
                // TODO: Exception Constants
                throw new ArgumentException("Category with this Id doesn't exist!");
            }

            var category = await this.categoryRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            var categoryServiceModel = new CategoryServiceModel
            {
                Id = category.Id,
                Name = category.Name,
            };

            return categoryServiceModel;
        }

        public IEnumerable<CategoryServiceModel> GetAll()
        {
            return this.categoryRepository
                .AllAsNoTracking()
                .Select(c => new CategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();
        }

        public async Task<bool> ContainsByIdAsync(int id)
            => await this.categoryRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id) != null;
    }
}
