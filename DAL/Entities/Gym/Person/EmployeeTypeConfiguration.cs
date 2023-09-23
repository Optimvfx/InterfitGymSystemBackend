using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person;

public class EmployeeTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasOne(e => e.Timetable)
            .WithMany(t => t.Employee)
            .HasForeignKey(e => e.TimetableId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Position)
            .WithMany(p => p.Employeers)
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(e => e.Vacation)
            .WithOne(v => v.Employee)
            .HasForeignKey<Employee>(e => e.VacationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.Personnel)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}