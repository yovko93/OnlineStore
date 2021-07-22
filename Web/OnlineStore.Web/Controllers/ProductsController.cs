namespace OnlineStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineStore.Data;
    using OnlineStore.Data.Models;
    using OnlineStore.Services.Data;
    using OnlineStore.Web.ViewModels.Categories;
    using OnlineStore.Web.ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            ApplicationDbContext data,
            IProductsService productsService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.data = data;
        }

        [Authorize]
        public IActionResult Create() => this.View(new CreateProductFormModel
        {
            Categories = this.GetProductCategories(),
            SubCategories = this.GetProductSubCategories(),
        });

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductFormModel productModel)
        {
            if (!this.data.Categories.Any(c => c.Id == productModel.CategoryId))
            {
                this.ModelState.AddModelError(nameof(productModel.CategoryId), "Category does not exist.");
            }

            if (!this.data.SubCategories.Any(s => s.Id == productModel.SubCategoryId))
            {
                this.ModelState.AddModelError(nameof(productModel.SubCategoryId), "SubCategory does not exist.");
            }

            if (!this.ModelState.IsValid)
            {
                productModel.Categories = this.GetProductCategories();

                productModel.SubCategories =
                    this.GetProductSubCategories();

                return this.Redirect("/Products/Create");
            }

            // var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            // productModel.UserId = userId;
            await this.productsService.CreateAsync(productModel);

            return this.RedirectToAction("Index", "Home");

            // return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllProductsQueryModel query)
        {
            var queryResult = this.productsService.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductsPerPage);

            var productNames = this.productsService.AllProductNames();

            query.Names = productNames;
            query.TotalProducts = queryResult.TotalProducts;
            query.Products = queryResult.Products;

            return this.View(query);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!this.productsService.Contains(id))
            {
                return this.Redirect("/Home/Index");
            }

            var loggedInUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = await this.productsService.GetByIdAsync(id);
            var user = await this.userManager.FindByIdAsync(product.UserId);

            //if (user.Id != loggedInUserId)
            //{
            //    // TODO:
            //    return this.Redirect("/");
            //}

            var category = await this.categoriesService.GetByIdAsync(product.Id);

            var viewModel = new DetailsViewModel()
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
                CategoryName = category.Name,
                SubCategoryName = null,
                SubCategoryId = product.SubCategoryId,
            };

            this.ViewData["loggedUserId"] =
                this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return this.View(viewModel);
        }

        private IEnumerable<CategoryViewModel> GetProductCategories()
        {
            return this.data
                .Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                })
                .ToList();
        }

        private IEnumerable<SubCategoryViewModel> GetProductSubCategories()
        {
            return this.data
                .SubCategories
                .Select(s => new SubCategoryViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .Distinct()
                .OrderBy(s => s.Name)
                .ToList();
        }
    }
}
