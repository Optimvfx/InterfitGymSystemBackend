using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Access;

public class AccessEntityTypeConfiguration : IEntityTypeConfiguration<Access>
{
    public void Configure(EntityTypeBuilder<Access> builder)
    {
        builder.HasMany(a => a.ApiKeys)
            .WithOne(a => a.Access)
            .HasForeignKey(a => a.AccessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}