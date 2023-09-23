using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Guarantee;

public class StoragePlaceConfiguration : IEntityTypeConfiguration<StoragePlace>
{
    public void Configure(EntityTypeBuilder<StoragePlace> builder)
    {
        builder.HasMany(s => s.Guarantees)
            .WithOne(g => g.StoragePlace)
            .HasForeignKey(g => g.StoragePlaceId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}