using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware.Breakdown;

public class BreakdownConfiguration : IEntityTypeConfiguration<Breakdown>
{
    public void Configure(EntityTypeBuilder<Breakdown> builder)
    {
        builder.HasOne(b => b.Type)
            .WithMany(b => b.Breakdowns)
            .HasForeignKey(b => b.TypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.DetectedAt)
            .WithMany(t => t.Breakdowns)
            .HasForeignKey(b => b.DetectedAtId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}