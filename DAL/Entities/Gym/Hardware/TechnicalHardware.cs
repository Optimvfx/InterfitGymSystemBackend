using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.Guarantee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware;

[Table("TechnicalHardwares")]
public class TechnicalHardware : GuaranteePossible
{
    [Required] public uint BuyPrice { get; set; }
    
    [Required] public Guid TechnicalHardwareInformationId { get; set; }
    public virtual TechnicalHardwareInformation TechnicalHardwareInformation { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}

public class TechnicalHardwareConfiguration : IEntityTypeConfiguration<TechnicalHardware>
{
    public void Configure(EntityTypeBuilder<TechnicalHardware> builder)
    {
        builder.HasOne(t => t.TechnicalHardwareInformation)
            .WithMany()
            .HasForeignKey(t => t.TechnicalHardwareInformationId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.TechnicalHardware)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}