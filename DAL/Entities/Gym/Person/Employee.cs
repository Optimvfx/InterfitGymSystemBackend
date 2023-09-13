using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Gym.Person.Employeers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person;

[Table("Employeers")]
public class Employee : Person
{
    [Required] public Guid TimetableId { get; set; }
    public virtual TimetableEntity Timetable { get; set; }
    
    [Required] public Guid PositionId { get; set; }
    public virtual Position Position { get; set; }
    
    [AllowNull] public Guid? VacationId { get; set; }
    public virtual Vacation Vacation { get; set; }
    
    [Required] public uint Wages { get; set; }
    
    [Required] public bool LeaveCompany { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}

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