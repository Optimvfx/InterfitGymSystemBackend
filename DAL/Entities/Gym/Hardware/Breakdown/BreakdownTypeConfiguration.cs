using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware.Breakdown;

public class BreakdownTypeConfiguration : IEntityTypeConfiguration<BreakdownType>
{
    public void Configure(EntityTypeBuilder<BreakdownType> builder)
    {
        builder.HasMany(b => b.Breakdowns)
            .WithOne(b => b.Type)
            .HasForeignKey(b => b.TypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}