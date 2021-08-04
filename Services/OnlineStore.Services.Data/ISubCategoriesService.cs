namespace OnlineStore.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OnlineStore.Web.ViewModels.SubCategories;

    public interface ISubCategoriesService
    {
        Task<bool> ContainsByIdAsync(int id);

        Task<SubCategoryServiceModel> GetByIdAsync(int id);

        IEnumerable<SubCategoryServiceModel> GetAllByCategoryId(int categoryId);

        IEnumerable<SubCategoryServiceModel> GetAll();
    }
}
