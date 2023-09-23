using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware;

public class ConsumableInformationConfiguration : IEntityTypeConfiguration<ConsumableInformation>
{
    public void Configure(EntityTypeBuilder<ConsumableInformation> builder)
    {
        builder.HasOne(t => t.Manufacturer)
            .WithMany()
            .HasForeignKey(t => t.ManufacturerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}