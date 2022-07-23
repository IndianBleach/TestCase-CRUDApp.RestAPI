using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestAPI.Data.Entities.OrderEntity;

namespace RestAPI.Data.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Provider)
                .WithMany(x => x.Orders)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
