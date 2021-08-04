namespace OnlineStore.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OnlineStore.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        Task<bool> ContainsByIdAsync(int id);

        Task<CategoryServiceModel> GetByIdAsync(int id);

        IEnumerable<CategoryServiceModel> GetAll();
    }
}
