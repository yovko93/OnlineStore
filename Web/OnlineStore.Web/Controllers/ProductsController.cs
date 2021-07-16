namespace OnlineStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineStore.Data;
    using OnlineStore.Services.Data;
    using OnlineStore.Web.ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IProductsService productsService;

        public ProductsController(ApplicationDbContext data, IProductsService productsService)
        {
            this.productsService = productsService;
            this.data = data;
        }

        [Authorize]
        public IActionResult Create() => this.View(new CreateProductFormModel
        {
            Categories = this.GetProductCategories(),
        });

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductFormModel productModel)
        {
            if (!this.data.Categories.Any(c => c.Id == productModel.CategoryId))
            {
                this.ModelState.AddModelError(nameof(productModel.CategoryId), "Category does not exist.");
            }

            if (!this.ModelState.IsValid)
            {
                productModel.Categories = this.GetProductCategories();

                return this.Redirect("/Products/Create");
            }

            await this.productsService.CreateAsync(productModel);

            return this.RedirectToAction("Index", "Home");

            // return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            return this.View();
        }

        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
        {
            return this.data
                .Categories
                .Select(c => new ProductCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                })
                .ToList();
        }
    }
}
