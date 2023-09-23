using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person;

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