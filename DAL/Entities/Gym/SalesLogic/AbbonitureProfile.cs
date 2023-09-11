using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.SalesLogic;

[Table("AbbonitureProfiles")]
public class AbbonitureProfile : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }   
    
    [Required] public uint Price { get; set; }
    
    [AllowNull] public uint? VisitLimit { get; set; }
    [AllowNull] public uint? DateLimitInDays { get; set; }
    
    [Required] public bool OnSale { get; set; }
    
    public virtual ICollection<TradeTransaction> TradeTransactions { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}

public class AbbonitureTypeConfiguration : IEntityTypeConfiguration< AbbonitureProfile>
{
    public void Configure(EntityTypeBuilder< AbbonitureProfile> builder)
    {
        builder.HasMany(a => a.TradeTransactions)
            .WithOne(t => t.AbbonitureProfile)
            .HasForeignKey(t => t.AbbonitureProfileId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.AbbonitureProfiles)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}