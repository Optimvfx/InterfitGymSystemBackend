using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Gym.Person;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Primary;

[Table("Card")]
public class Card: IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public DateTime CreationDate { get; set; }
    [AllowNull] public DateTime? ExpirationDate { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym.Gym Gym { get; set; }
    
    [AllowNull] public Guid? OwnerId { get; set; }
    public virtual Person? Owner { get; set; }
}

public class CardTypeConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasOne(c => c.Owner)
            .WithMany(o => o.Cards)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.Cards)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}