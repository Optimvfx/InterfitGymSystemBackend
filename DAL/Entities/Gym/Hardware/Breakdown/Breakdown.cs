using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware.Breakdown;

[Table("Breakdowns")]
public class Breakdown
{
    [Key] public Guid Id { get; set; }
    
    [Required] public DateOnly DetectedDate { get; set; }
    [AllowNull] public DateOnly? FixedDate { get; set; }
    
    [Required] public Guid TypeId { get; set; }
    public virtual BreakdownType Type { get; set; }
    
    [Required] public Guid DetectedAtId { get; set; }
    public virtual TrainingDevice DetectedAt { get; set; }
}

public class BreakdownConfiguration : IEntityTypeConfiguration<Breakdown>
{
    public void Configure(EntityTypeBuilder<Breakdown> builder)
    {
        builder.HasOne(b => b.Type)
            .WithMany(b => b.Breakdowns)
            .HasForeignKey(b => b.TypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.DetectedAt)
            .WithMany(t => t.Breakdowns)
            .HasForeignKey(b => b.DetectedAtId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}