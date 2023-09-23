using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;
using DAL.Entities.Structs;

namespace DAL.Entities.Gym.Person.Employeers;

[Table("Timetables")]
public class TimetableEntity : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    [AllowNull] public string Title { get; set; }
    
    [Required] public DayGraphic MondaySchedule { get; set; }
    [Required] public DayGraphic TuesdaySchedule { get; set; }
    [Required] public DayGraphic WednesdaySchedule { get; set; }
    [Required] public DayGraphic ThursdaySchedule { get; set; }
    [Required] public DayGraphic FridaySchedule { get; set; }
    [Required] public DayGraphic SaturdaySchedule { get; set; }
    [Required] public DayGraphic SundaySchedule { get; set; }
    
    public virtual ICollection<Employee> Employee { get; set; }
}