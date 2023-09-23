using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Primary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Access.AccessType;

[Table("ApiAdministrator")]
public class ApiAdministrator : Access
{
    public virtual ICollection<ApiKey> CreatedApiKeys { get; set; }
}

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