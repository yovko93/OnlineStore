namespace OnlineStore.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineStore.Services.Data;
    using OnlineStore.Web.ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductFormModel productModel)
        {
            if (!this.ModelState.IsValid)
            {
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

    }
}
