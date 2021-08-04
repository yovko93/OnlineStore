namespace OnlineStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using OnlineStore.Data.Common.Repositories;
    using OnlineStore.Data.Models;
    using OnlineStore.Web.ViewModels.SubCategories;

    public class SubCategoriesService : ISubCategoriesService
    {
        // TODO: move to separate class
        private const string InvalidIdErrorMessage = "Subcategory with this Id doesn't exist";

        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public SubCategoriesService(IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<bool> ContainsByIdAsync(int id)
            => await this.subCategoryRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id) != null;

        public IEnumerable<SubCategoryServiceModel> GetAllByCategoryId(int categoryId)
        {
            return this.subCategoryRepository
                .AllAsNoTracking()
                .Where(s => s.CategoryId == categoryId)
                .Select(s => new SubCategoryServiceModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    CategoryId = s.CategoryId,
                });
        }

        public IEnumerable<SubCategoryServiceModel> GetAll()
        {
            return this.subCategoryRepository
                .AllAsNoTracking()
                .Select(c => new SubCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();
        }

        public async Task<SubCategoryServiceModel> GetByIdAsync(int id)
        {
            if (!await this.ContainsByIdAsync(id))
            {
                throw new ArgumentException(InvalidIdErrorMessage);
            }

            var subCategory = await
                this.subCategoryRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            var subCategoryServiceModel = new SubCategoryServiceModel()
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
            };

            return subCategoryServiceModel;
        }
    }
}
