using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.SalesLogic;

public class OrderTypeEntityTypeConfiguration : IEntityTypeConfiguration<OrderType>
{
    public void Configure(EntityTypeBuilder<OrderType> builder)
    {
        builder.HasMany(t => t.Orders)
            .WithOne(o => o.Type)
            .HasForeignKey(o => o.TypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}