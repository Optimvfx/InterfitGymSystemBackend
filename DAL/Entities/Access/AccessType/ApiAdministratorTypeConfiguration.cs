using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Access.AccessType;

public class ApiAdministratorTypeConfiguration : IEntityTypeConfiguration<ApiAdministrator>
{
    public void Configure(EntityTypeBuilder<ApiAdministrator> builder)
    {
        builder.HasMany(a => a.CreatedApiKeys)
            .WithOne(a => a.Author)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}