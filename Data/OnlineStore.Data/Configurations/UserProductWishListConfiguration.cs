namespace OnlineStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OnlineStore.Data.Models;

    public class UserProductWishListConfiguration : IEntityTypeConfiguration<UserProductWishList>
    {
        public void Configure(EntityTypeBuilder<UserProductWishList> entity)
        {
            entity
               .HasKey(ua => new { ua.UserId, ua.ProductId });

            entity
                .HasOne(ua => ua.User)
                .WithMany(u => u.WishList)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
