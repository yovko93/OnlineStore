namespace OnlineStore.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 3;

        public string Title { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; set; }

        public ProductSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalProducts { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}
