using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware;

[Table("TechnicalHardwaresInformation")]
public class TechnicalHardwareInformation : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }
    [AllowNull] public string? Description { get; set; }
    
    [Required] public Guid ManufacturerId { get; set; }
    public virtual Company Manufacturer { get; set; }
}

public class TechnicalHardwareInformationConfiguration : IEntityTypeConfiguration<TechnicalHardwareInformation>
{
    public void Configure(EntityTypeBuilder<TechnicalHardwareInformation> builder)
    {
        builder.HasOne(t => t.Manufacturer)
            .WithMany()
            .HasForeignKey(t => t.ManufacturerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}