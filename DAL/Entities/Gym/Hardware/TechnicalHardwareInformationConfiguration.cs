using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware;

public class TechnicalHardwareInformationConfiguration : IEntityTypeConfiguration<TechnicalHardwareInformation>
{
    public void Configure(EntityTypeBuilder<TechnicalHardwareInformation> builder)
    {
        builder.HasOne(t => t.Manufacturer)
            .WithMany()
            .HasForeignKey(t => t.ManufacturerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}