using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Interfaces;
using DAL.Entities.Structs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Employeers;

[Table("Timetables")]
public class Timetable : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public DayGraphic MondaySchedule { get; set; }
    [Required] public DayGraphic TuesdaySchedule { get; set; }
    [Required] public DayGraphic WednesdaySchedule { get; set; }
    [Required] public DayGraphic ThursdaySchedule { get; set; }
    [Required] public DayGraphic FridaySchedule { get; set; }
    [Required] public DayGraphic SaturdaySchedule { get; set; }
    [Required] public DayGraphic SundaySchedule { get; set; }
    
    public virtual ICollection<Employee> Employee { get; set; }
}

public class TimetableTypeConfiguration : IEntityTypeConfiguration<Timetable>
{
    public void Configure(EntityTypeBuilder<Timetable> builder)
    {
        builder.HasMany(t => t.Employee)
            .WithOne(e => e.Timetable)
            .HasForeignKey(e => e.TimetableId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}