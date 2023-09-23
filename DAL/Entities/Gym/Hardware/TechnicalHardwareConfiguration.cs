using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware;

public class TechnicalHardwareConfiguration : IEntityTypeConfiguration<TechnicalHardware>
{
    public void Configure(EntityTypeBuilder<TechnicalHardware> builder)
    {
        builder.HasOne(t => t.TechnicalHardwareInformation)
            .WithMany()
            .HasForeignKey(t => t.TechnicalHardwareInformationId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.TechnicalHardware)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}