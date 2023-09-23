using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Gym.Person.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person;

[Table("Clients")]
public class Client : Person
{
    [AllowNull] public Guid? AbbonnitureId { get; set; }
    public virtual Abbonniture Abbonniture { get; set; }
    
    [AllowNull] public string? TelegramId { get; set; }
}

public class ClientTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasOne(c => c.Abbonniture)
            .WithOne(a => a.Client)
            .HasForeignKey<Client>(c => c.AbbonnitureId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}