using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Employeers;

public class TerminalAdministratorTypeConfiguration : IEntityTypeConfiguration<TerminalAdministrator>
{
    public void Configure(EntityTypeBuilder<TerminalAdministrator> builder)
    {
        builder.HasOne(t => t.Terminal)
            .WithOne(t => t.Administrator)
            .HasForeignKey<TerminalAdministrator>(t => t.TerminalId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}