namespace OnlineStore.Data.Models
{
    using OnlineStore.Data.Common.Models;

    public class CategoryProduct : BaseModel<int>
    {
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
