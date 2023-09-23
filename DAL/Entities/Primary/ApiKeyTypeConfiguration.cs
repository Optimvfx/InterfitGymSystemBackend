using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Primary;

public class ApiKeyTypeConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasOne(a => a.Access)
            .WithMany(a => a.ApiKeys)
            .HasForeignKey(a => a.AccessId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Author)
            .WithMany(a => a.CreatedApiKeys)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}