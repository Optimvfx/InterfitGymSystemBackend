using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware;

public class TrainingDeviceConfiguration : IEntityTypeConfiguration<TrainingDevice>
{
    public void Configure(EntityTypeBuilder<TrainingDevice> builder)
    {
        builder.HasMany(t => t.Breakdowns)
            .WithOne(b => b.DetectedAt)
            .HasForeignKey(b => b.DetectedAtId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.TrainingDevices)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}