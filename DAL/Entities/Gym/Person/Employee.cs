using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Gym.Person.Employeers;

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