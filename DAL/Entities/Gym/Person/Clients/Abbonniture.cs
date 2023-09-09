using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Clients;

[Table("Abbonnitures")]
public class Abbonniture
{
    [Key] public Guid Id { get; set; }
    
    [Required] public uint VisitsRemaining { get; set; }
    [Required] public DateOnly ExpirationDate { get; set; }
    
    public Guid ClientId { get; set; }
    public virtual Client Client { get; set; }
}

public class AbbonnitureTypeConfiguration : IEntityTypeConfiguration<Abbonniture>
{
    public void Configure(EntityTypeBuilder<Abbonniture> builder)
    {
        builder.HasOne(a => a.Client)
            .WithOne(c => c.Abbonniture)
            .HasForeignKey<Abbonniture>(a => a.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}