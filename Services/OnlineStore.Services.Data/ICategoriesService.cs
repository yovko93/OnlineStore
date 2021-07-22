namespace OnlineStore.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineStore.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        Task<bool> ContainsByIdAsync(int id);

        Task<CategoryServiceModel> GetByIdAsync(int id);

        IQueryable<CategoryServiceModel> GetAll();
    }
}
