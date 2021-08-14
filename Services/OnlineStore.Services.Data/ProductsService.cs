namespace OnlineStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<string> AllProductNames()
            => this.productsRepository
                .AllAsNoTracking()
                .Select(p => p.Name)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public async Task CreateAsync(CreateProductFormModel productModel)
        {
            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                OldPrice = productModel.OldPrice,
                Color = productModel.Color,
                Size = productModel.Size,
                ImageUrl = productModel.ImageUrl,
                UserId = productModel.UserId,
                CategoryId = productModel.CategoryId,
                SubCategoryId = productModel.SubCategoryId,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task DeleteById(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductViewServiceModel> GetByIdAsync(int id)
        {
            if (!this.Contains(id))
            {
                throw new ArgumentException("Product Id is invalid!");
            }

            var product = await this.productsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return new ProductViewServiceModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                OldPrice = product.OldPrice,
                Color = product.Color,
                Size = product.Size,
                ImageUrl = product.ImageUrl,
                UserId = product.UserId,
                CategoryId = product.CategoryId,
                SubCategoryId = product.SubCategoryId,
            };
        }

        public ProductQueryServiceModel GetAll(string name, string searchTerm, ProductSorting sorting, int currentPage, int productsPerPage)
        {
            var productsQuery = this.productsRepository
                .AllAsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                productsQuery = productsQuery.Where(p => p.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    (p.Name + " " + p.Category.Name).ToLower().Contains(searchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.Name => productsQuery.OrderByDescending(p => p.Name).ThenBy(c => c.Category.Name),
                ProductSorting.Price => productsQuery.OrderBy(p => p.Price),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id),
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                })
                .ToList();

            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                Products = products,
            };
        }

        public bool Contains(int productId)
            => this.productsRepository
            .AllAsNoTracking()
            .Any(p => p.Id == productId);

        public async Task EditAsync(EditProductServiceModel productModel)
        {
            throw new NotImplementedException();
        }
    }
}
