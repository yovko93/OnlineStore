namespace OnlineStore.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OnlineStore.Web.ViewModels.Products;

    public interface IProductsService
    {
        Task CreateAsync(CreateProductFormModel productModel);

        Task EditAsync(EditProductServiceModel productModel);

        ProductQueryServiceModel All(
            string name,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage);

        Task<ProductViewServiceModel> GetByIdAsync(int id);

        Task DeleteById(int productId);

        IEnumerable<string> AllProductNames();

        bool Contains(int productId);
    }
}
