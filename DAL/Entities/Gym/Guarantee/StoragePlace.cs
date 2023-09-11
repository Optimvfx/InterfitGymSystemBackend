using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Guarantee;

[Table("StoragePlaces")]
public class StoragePlace : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }
    [AllowNull] public string? Description { get; set; }
    
    public virtual ICollection<Guarantee> Guarantees { get; set; }
}

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