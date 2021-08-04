namespace OnlineStore.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineStore.Services.Data;

    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class UsersController : Controller
    {
        private readonly IProductsService productsService;

        public UsersController(
            IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Wishlist(int page = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (page <= 0)
            {
                return this.Redirect("/Users/Wishlist");
            }


            return null;
        }

        public async Task<IActionResult> AddToWishlist()
        {
            return null;
        }

        public async Task<IActionResult> RemoveFromWishlist()
        {
            return null;
        }
    }
}
