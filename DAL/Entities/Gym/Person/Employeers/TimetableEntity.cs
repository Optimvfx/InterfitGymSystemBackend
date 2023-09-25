using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.Person.Employeers;

[Table("Timetables")]
public class TimetableEntity : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    [AllowNull] public string Title { get; set; }
    
    public virtual ICollection<Employee> Employee { get; set; }
    public virtual ICollection<DayGraphic> DayGraphics { get; set; }
}