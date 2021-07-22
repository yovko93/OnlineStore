namespace OnlineStore.Web.ViewComponent
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineStore.Services.Data;

    [ViewComponent(Name = "CategoriesDropdown")]
    public class CategoriesDropdownViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesDropdownViewComponent(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = this.categoriesService.GetAll().OrderBy(c => c.Name).ToList();

            return this.View(categories);
        }
    }
}
