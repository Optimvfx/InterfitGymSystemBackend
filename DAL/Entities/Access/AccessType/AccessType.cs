using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Access.AccessType;

[Table("AccessTypes")]
public class AccessType
{
    [Key] public Guid Id { get; set; }

    public virtual ICollection<Access> Accesses { get; set; }
}  

public class AccessTypeEntityTypeConfiguration : IEntityTypeConfiguration<AccessType>
{
    public void Configure(EntityTypeBuilder<AccessType> builder)
    {
        builder.HasMany(t => t.Accesses)
            .WithOne(a => a.Type)
            .HasForeignKey(a => a.TypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}