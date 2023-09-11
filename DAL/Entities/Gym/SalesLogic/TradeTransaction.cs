using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.Person;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.SalesLogic;

[Table("TradeTransactions")]
public class TradeTransaction : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public DateTime DateTime { get; set; }
    
    [Required] public Guid AbbonitureProfileId { get; set; }
    public virtual AbbonitureProfile AbbonitureProfile { get; set; }
    
    [Required] public Guid ClientId { get; set; }
    public virtual Client Client { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}

public class TradeTransactionEntityTypeConfiguration : IEntityTypeConfiguration<TradeTransaction>
{
    public void Configure(EntityTypeBuilder<TradeTransaction> builder)
    {
        builder.HasOne(t => t.Client)
            .WithOne()
            .HasForeignKey<TradeTransaction>(t => t.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.TradeTransactions)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}