using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware;

public class TrainingDeviceInformationConfiguration : IEntityTypeConfiguration<TrainingDeviceInformation>
{
    public void Configure(EntityTypeBuilder<TrainingDeviceInformation> builder)
    {
        builder.HasOne(t => t.Manufacturer)
            .WithMany()
            .HasForeignKey(t => t.ManufacturerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(t => t.PosibleBreakdowns)
            .WithMany();
    }
}