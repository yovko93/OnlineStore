namespace OnlineStore.Data.Models
{
    using OnlineStore.Data.Common.Models;

    public class UserProductWishList : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
