using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Gym.Guarantee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware;

[Table("TrainingDevices")]
public class TrainingDevice : GuaranteePossible
{
    [Required] public uint BuyPrice { get; set; }
    
    [Required] public Guid TrainingDeviceInformationId { get; set; }
    public virtual TrainingDeviceInformation TrainingDeviceInformation { get; set; }
    
    public virtual ICollection<Breakdown.Breakdown> Breakdowns { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}

public class TrainingDeviceConfiguration : IEntityTypeConfiguration<TrainingDevice>
{
    public void Configure(EntityTypeBuilder<TrainingDevice> builder)
    {
        builder.HasMany(t => t.Breakdowns)
            .WithOne(b => b.DetectedAt)
            .HasForeignKey(b => b.DetectedAtId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.TrainingDevices)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}