namespace OnlineStore.Data.Models
{
    using System;

    using OnlineStore.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public int Quantity { get; set; }

        public decimal OrderTotalPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerId { get; set; }

        public ApplicationUser Customer { get; set; }
    }
}
