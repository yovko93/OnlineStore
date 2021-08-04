namespace OnlineStore.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using OnlineStore.Web.ViewModels.Categories;
    using OnlineStore.Web.ViewModels.SubCategories;

    public class CreateProductFormModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        // [Required]
        // [StringLength(30, MinimumLength = 3)]
        // public string Title { get; set; }
        public string Color { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }

        public IEnumerable<SubCategoryServiceModel> SubCategories { get; set; }
    }
}
