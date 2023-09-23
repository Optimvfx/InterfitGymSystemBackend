using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Guarantee;

public class GuaranteeConfiguration : IEntityTypeConfiguration<Guarantee>
{
    public void Configure(EntityTypeBuilder<Guarantee> builder)
    {
        builder.HasOne(g => g.Target)
            .WithOne(t => t.Guarantee)
            .HasForeignKey<Guarantee>(g => g.TargetId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(g => g.StoragePlace)
            .WithMany(t => t.Guarantees)
            .HasForeignKey(g => g.StoragePlaceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}