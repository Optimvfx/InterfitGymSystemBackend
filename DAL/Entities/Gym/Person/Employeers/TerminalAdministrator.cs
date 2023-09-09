using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Access.AccessType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Employeers;

[Table("TerminalAdministrators")]
public class TerminalAdministrator : Position
{
    [Required] public Guid TerminalId { get; set; }
    public virtual Terminal Terminal { get; set; }
}

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