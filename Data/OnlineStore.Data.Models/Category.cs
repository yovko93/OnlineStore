namespace OnlineStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using OnlineStore.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.CategoryProducts = new HashSet<CategoryProduct>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public virtual IEnumerable<CategoryProduct> CategoryProducts { get; set; }
    }
}
