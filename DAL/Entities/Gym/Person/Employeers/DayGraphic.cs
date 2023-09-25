using System.ComponentModel.DataAnnotations;

namespace DAL.Entities.Gym.Person.Employeers;

public class DayGraphic
{
    [Required] public TimeOnly StartWorkAt { get; set; }
    [Required] public TimeOnly StopWorkAt { get; set; }

    [Required] public DayOfWeek DayOfWeek { get; set; }
    
    [Required] public Guid TimetableId { get; set; }
    public virtual TimetableEntity Timetable { get; set; }
}