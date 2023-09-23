using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Employeers;

public class VacationTypeConfiguration : IEntityTypeConfiguration<Vacation>
{
    public void Configure(EntityTypeBuilder<Vacation> builder)
    {
        builder.HasOne(v => v.Employee)
            .WithOne(e => e.Vacation)
            .HasForeignKey<Vacation>(v => v.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}