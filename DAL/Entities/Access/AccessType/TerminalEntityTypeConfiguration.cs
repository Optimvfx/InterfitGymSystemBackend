using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Access.AccessType;

public class TerminalEntityTypeConfiguration : IEntityTypeConfiguration<Terminal>
{
    public void Configure(EntityTypeBuilder<Terminal> builder)
    {
        builder.HasOne(t => t.Administrator)
            .WithOne(a => a.Terminal)
            .HasForeignKey<Terminal>(a => a.AdministratorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Gym)
            .WithMany(g => g.Terminals)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}