namespace OnlineStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using OnlineStore.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(255)]
        public string Text { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
