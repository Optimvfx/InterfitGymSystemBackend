using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.SalesLogic;

public class AbbonitureTypeConfiguration : IEntityTypeConfiguration< AbbonitureProfile>
{
    public void Configure(EntityTypeBuilder< AbbonitureProfile> builder)
    {
        builder.HasMany(a => a.TradeTransactions)
            .WithOne(t => t.AbbonitureProfile)
            .HasForeignKey(t => t.AbbonitureProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}