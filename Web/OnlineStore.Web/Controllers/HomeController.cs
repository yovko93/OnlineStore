namespace OnlineStore.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using OnlineStore.Data;
    using OnlineStore.Web.ViewModels;
    using OnlineStore.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext data;

        public HomeController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            var totalProducts = this.data.Products.Count();

            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductIndexViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                })
                .Take(3)
                .ToList();

            return this.View(new IndexViewModel
            {
                TotalProducts = totalProducts,
                Products = products,
            });
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
