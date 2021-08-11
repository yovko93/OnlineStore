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
    using OnlineStore.Web.ViewModels.SubCategories;

    [AutoValidateAntiforgeryToken]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly ISubCategoriesService subCategoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            ApplicationDbContext data,
            IProductsService productsService,
            ICategoriesService categoriesService,
            ISubCategoriesService subCategoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.subCategoriesService = subCategoriesService;
            this.userManager = userManager;
            this.data = data;
        }

        [Authorize]
        public IActionResult Create() => this.View(new CreateProductFormModel
        {
        //    Categories = this.categoriesService.GetAll(),
        //    SubCategories = this.subCategoriesService.GetAll(),
        });

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductFormModel productModel)
        {
            //if (!this.data.Categories.Any(c => c.Id == productModel.CategoryId))
            //{
            //    this.ModelState.AddModelError(nameof(productModel.CategoryId), "Category does not exist.");
            //}

            //if (!this.data.SubCategories.Any(s => s.Id == productModel.SubCategoryId))
            //{
            //    this.ModelState.AddModelError(nameof(productModel.SubCategoryId), "SubCategory does not exist.");
            //}

            if (!this.ModelState.IsValid)
            {
                //productModel.Categories = this.categoriesService.GetAll();

                //productModel.SubCategories = this.subCategoriesService.GetAll();

                return this.Redirect("/Products/Create");
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

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

            // if (user.Id != loggedInUserId)
            // {
            //    // TODO:
            //    return this.Redirect("/");
            // }
            var category = await this.categoriesService.GetByIdAsync(product.CategoryId);

            var subCategory = await this.subCategoriesService
                .GetByIdAsync(product.SubCategoryId);

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
                CategoryId = category.Id,
                CategoryName = category.Name,
                SubCategoryId = subCategory.Id,
                SubCategoryName = subCategory.Name,
            };

            this.ViewData["loggedUserId"] =
                this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return this.View(viewModel);
        }

        // TODO: remove
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

        // TODO: remove
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
