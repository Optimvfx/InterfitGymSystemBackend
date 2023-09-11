using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Hardware;

[Table("Consumables")]
public class Consumable : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public uint Count { get; set; }
    [Required] public uint AvgBuyPrice { get; set; }
    
    [Required] public Guid ConsumableInformationId { get; set; }
    public virtual ConsumableInformation ConsumableInformation { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}

public class ConsumableConfiguration : IEntityTypeConfiguration<Consumable>
{
    public void Configure(EntityTypeBuilder<Consumable> builder)
    {
        builder.HasOne(t => t.ConsumableInformation)
            .WithMany()
            .HasForeignKey(t => t.ConsumableInformationId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.Consumables)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}