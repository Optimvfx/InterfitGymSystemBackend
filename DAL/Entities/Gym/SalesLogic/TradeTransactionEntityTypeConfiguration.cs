using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.SalesLogic;

public class TradeTransactionEntityTypeConfiguration : IEntityTypeConfiguration<TradeTransaction>
{
    public void Configure(EntityTypeBuilder<TradeTransaction> builder)
    {
        builder.HasOne(t => t.Client)
            .WithOne()
            .HasForeignKey<TradeTransaction>(t => t.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.TradeTransactions)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}