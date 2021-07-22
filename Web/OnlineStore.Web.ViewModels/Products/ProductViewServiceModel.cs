namespace OnlineStore.Web.ViewModels.Products
{
    public class ProductViewServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public string UserId { get; set; }
    }
}
