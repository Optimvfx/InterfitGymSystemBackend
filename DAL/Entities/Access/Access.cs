using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Interfaces;
using DAL.Entities.Primary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Access;

[Table("Access")]
public class Access : IIndexSearchable
{
    [Key] public Guid Id { get; set; }

    [Required] public ICollection<ApiKey> ApiKeys { get; set; } = null!;
    
    [Required] public Guid TypeId { get; set; }
    public virtual AccessType.AccessType Type { get; set; }
}

public class AccessEntityTypeConfiguration : IEntityTypeConfiguration<Access>
{
    public void Configure(EntityTypeBuilder<Access> builder)
    {
        builder.HasMany(a => a.ApiKeys)
            .WithOne(a => a.Access)
            .HasForeignKey(a => a.AccessId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Type)
            .WithMany(t => t.Accesses)
            .HasForeignKey(a => a.TypeId);
    }
}