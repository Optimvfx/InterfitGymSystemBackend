using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Guarantee;

[Table("Guarantees")]
public class Guarantee : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public DateOnly ExpirationDate { get; set; }
    
    [Required] public Guid TargetId { get; set; }
    public virtual GuaranteePossible Target { get; set; }
    
    [Required] public Guid StoragePlaceId { get; set; }
    public virtual StoragePlace StoragePlace { get; set; }
}

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