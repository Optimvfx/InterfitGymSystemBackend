using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Clients;

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