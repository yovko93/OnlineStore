namespace OnlineStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OnlineStore.Data.Models;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product
               .HasMany(a => a.Comments)
               .WithOne(c => c.Product)
               .HasForeignKey(c => c.ProductId);

            product
                .HasOne(a => a.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(a => a.CategoryId);

            product
                .HasOne(a => a.SubCategory)
                .WithMany(c => c.Products)
                .HasForeignKey(a => a.SubCategoryId);
        }
    }
}
