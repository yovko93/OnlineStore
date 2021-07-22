namespace OnlineStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using OnlineStore.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Comments = new HashSet<Comment>();
            this.UsersWishList = new HashSet<UserProductWishList>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        // [Required]
        // [MaxLength(30)]
        // public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public int Quantity { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserProductWishList> UsersWishList { get; set; }

        // public Gender Gender { get; set; }
        // public int Views { get; set; }
        // public ProductCondition Condition { get; set; }
    }
}
