using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Guarantee;

public abstract class GuaranteePossible : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
 
    [AllowNull] public Guid? GuaranteeId { get; set; }
    public virtual Guarantee? Guarantee { get; set; }
}

public class GuaranteePossibleConfiguration : IEntityTypeConfiguration<GuaranteePossible>
{
    public void Configure(EntityTypeBuilder<GuaranteePossible> builder)
    {
        builder.HasOne(g => g.Guarantee)
            .WithOne(g => g.Target)
            .HasForeignKey<GuaranteePossible>(g => g.GuaranteeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}