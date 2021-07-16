namespace OnlineStore.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OnlineStore.Web.ViewModels.Products;

    public interface IProductsService
    {
        Task CreateAsync(CreateProductFormModel productModel);

        IEnumerable<ProductViewModel> GetAll();
    }
}
