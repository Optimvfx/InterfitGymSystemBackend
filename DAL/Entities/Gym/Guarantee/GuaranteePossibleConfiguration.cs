using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Guarantee;

public class GuaranteePossibleConfiguration : IEntityTypeConfiguration<GuaranteePossible>
{
    public void Configure(EntityTypeBuilder<GuaranteePossible> builder)
    {
        builder.HasOne(g => g.Guarantee)
            .WithOne(g => g.Target)
            .HasForeignKey<GuaranteePossible>(g => g.GuaranteeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}