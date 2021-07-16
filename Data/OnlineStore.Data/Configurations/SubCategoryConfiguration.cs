namespace OnlineStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OnlineStore.Data.Models;

    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> subCategory)
        {
            subCategory
              .HasOne(s => s.Category)
              .WithMany(c => c.SubCategories)
              .HasForeignKey(s => s.CategoryId);
        }
    }
}
