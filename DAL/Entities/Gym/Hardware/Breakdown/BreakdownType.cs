using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware.Breakdown;

[Table("BreakdownTypes")]
public class BreakdownType
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }
    [AllowNull] public string? Description { get; set; }

    public virtual ICollection<Breakdown> Breakdowns { get; set; }
}

public class BreakdownTypeConfiguration : IEntityTypeConfiguration<BreakdownType>
{
    public void Configure(EntityTypeBuilder<BreakdownType> builder)
    {
        builder.HasMany(b => b.Breakdowns)
            .WithOne(b => b.Type)
            .HasForeignKey(b => b.TypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}