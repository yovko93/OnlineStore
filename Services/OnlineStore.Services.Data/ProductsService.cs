namespace OnlineStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineStore.Data.Common.Repositories;
    using OnlineStore.Data.Models;
    using OnlineStore.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productsRepository = productRepository;
        }

        public async Task CreateAsync(CreateProductFormModel productModel)
        {
            var product = new Product
            {
                Title = productModel.Title,
                Description = productModel.Description,
                Price = productModel.Price,
                OldPrice = productModel.OldPrice,
                Quantity = productModel.Quantity,
                ImageUrl = productModel.ImageUrl,
                Gender = Enum.Parse<Gender>(productModel.Gender, true),
                UserId = productModel.UserId,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            var products = this.productsRepository
                .AllAsNoTracking()
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl,
                    Gender = p.Gender.ToString(),
                    Description = p.Description,
                    Price = p.Price,
                })
                .ToList();

            return products;
        }

        public ProductViewModel GetById(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
