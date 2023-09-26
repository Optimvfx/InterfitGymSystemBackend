using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.SalesLogic;

public class SaleEntityTypeConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasOne(t => t.Client)
            .WithOne()
            .HasForeignKey<Sale>(t => t.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.TradeTransactions)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}